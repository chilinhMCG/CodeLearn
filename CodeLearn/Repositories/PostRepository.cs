using CodeLearn.Data;
using CodeLearn.Data.OrderingQuery;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private class CommentCount
        {
            public Guid PostId { get; set; }
            public int Value { get; set; }
        }

        private class OverallRating
        {
            public Guid PostId { get; set; }

            public int RatingCount { get; set; }

            public float Value { get; set; }
        }

        public PostRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<Post> GetAsync(Guid userId, string slug)
        {
            using var context = DbContextFactory.CreateDbContext();
            return await context.Posts.FirstOrDefaultAsync(p => p.Slug == slug && p.UserId == userId);
        }

        public async Task<PostInfo> GetPostInfoAsync(Guid id)
        {
            using var context = DbContextFactory.CreateDbContext();
            Post post = await context.Posts.FindAsync(id);

            return await ToPostInfoAsync(context, post);
        }

        public async Task<Page<PostInfo>> GetPagePostInfo(int pageSize, int pageNumber, OrderingQueryDelegate<PostInfo> orderingQuery = null)
        {
            using var context = DbContextFactory.CreateDbContext();
            var allPosts = context.Posts.OrderByDescending(p => p.DateCreated);

            int pageCount = PaginationUtils.GetPageCount(pageSize, await allPosts.CountAsync());

            if (pageNumber > pageCount)
            {
                return new Page<PostInfo>
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = pageCount
                };
            }

            var postInfoListQuery = ToPostInfoListQuery(context, allPosts);

            if (orderingQuery != null)
            {
                postInfoListQuery = orderingQuery(new OrderingQuery<PostInfo>(postInfoListQuery))
                                                 .GetInnerQuery();
            }

            var postInfoList = await postInfoListQuery.TakeFromPage(pageNumber, pageSize)
                                                      .ToListAsync();

            postInfoList.ForEach(pi => pi.OverallRating = MathF.Round(pi.OverallRating, 2));

            return new Page<PostInfo>
            {
                Items = postInfoList,
                Size = pageSize,
                CurrentPage = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<Page<PostInfo>> GetPagePostInfoSearchByKeywords(int pageSize, int pageNumber,
            string keywordsText, OrderingQueryDelegate<PostInfo> orderingQuery = null)
        {
            if (string.IsNullOrWhiteSpace(keywordsText))
            {
                return new Page<PostInfo>
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = 0
                };
            }

            using var context = DbContextFactory.CreateDbContext();

            string[] keywords = keywordsText.Trim().RemoveDiacritics().Split();
            string tsQueryStr = string.Join(" | ", keywords);
            tsQueryStr += $" | {keywords.Last()}:*"; 

            var postsQuery = context.Posts.Where(
                p => p.TitleSearchVector
                      .Concat(p.ContentSearchVector)
                      .Matches(EF.Functions.ToTsQuery("simple", tsQueryStr)));

            int pageCount = PaginationUtils.GetPageCount(pageSize, await postsQuery.CountAsync());

            if (pageNumber > pageCount)
            {
                return new Page<PostInfo>
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = pageCount
                };
            }

            //order by relevance if client do not provide custom ordering
            if (orderingQuery == null)
            {
                postsQuery = postsQuery.OrderByDescending(
                    p => p.TitleSearchVector.SetWeight(NpgsqlTsVector.Lexeme.Weight.A)
                          .Concat(p.ContentSearchVector.SetWeight(NpgsqlTsVector.Lexeme.Weight.D))
                          .Rank(EF.Functions.ToTsQuery("simple", tsQueryStr)));
            }

            var postInfoListQuery = ToPostInfoListQuery(context, postsQuery);

            if (orderingQuery != null)
            {
                postInfoListQuery = orderingQuery(new OrderingQuery<PostInfo>(postInfoListQuery))
                                                 .GetInnerQuery();
            }

            var postInfoList = await postInfoListQuery.TakeFromPage(pageNumber, pageSize)
                                                      .ToListAsync();

            postInfoList.ForEach(p => p.OverallRating = MathF.Round(p.OverallRating, 2));

            return new Page<PostInfo>
            {
                Items = postInfoList,
                Size = pageSize,
                CurrentPage = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<Page<PostInfo>> GetPagePostInfoSearchByAuthorName(int pageSize, int pageNumber, 
            string authorName, OrderingQueryDelegate<PostInfo> orderQuery = null)
        {
            authorName = authorName.RemoveDiacritics().Trim();

            if (string.IsNullOrWhiteSpace(authorName))
            {
                return new Page<PostInfo>
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = 0
                };
            }

            using var context = DbContextFactory.CreateDbContext();

            string pattern = $"%{authorName}%";

            var usersQuery = context.Users.Where(u => EF.Functions.ILike(u.Name, pattern));

            var postsQuery = from post in context.Posts
                             join user in usersQuery
                             on post.UserId equals user.Id
                             select post;

            int pageCount = PaginationUtils.GetPageCount(pageSize, await postsQuery.CountAsync());

            if (pageNumber > pageCount)
            {
                return new Page<PostInfo>
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = pageCount
                };
            }

            var postInfoListQuery = ToPostInfoListQuery(context, postsQuery);

            if (orderQuery != null)
            {
                postInfoListQuery = orderQuery(new OrderingQuery<PostInfo>(postInfoListQuery))
                                                 .GetInnerQuery();
            }

            var postInfoList = await postInfoListQuery.TakeFromPage(pageNumber, pageSize)
                                                      .ToListAsync();

            FormatOverallRating(postInfoList);

            return new Page<PostInfo>
            {
                Items = postInfoList,
                Size = pageSize,
                CurrentPage = pageNumber,
                PageCount = pageCount
            };
        }

        private static async Task<PostInfo> ToPostInfoAsync(ApplicationDBContext context, Post post)
        {
            var user = await context.Users.FindAsync(post.UserId);
            var ratings = context.PostRatings.Where(pr => pr.PostId == post.Id);

            int ratingSum = await ratings.Select(pr => pr.Value).SumAsync(v => v);
            int ratingCount = await ratings.CountAsync();
            float overallRating = ratingCount == 0 ? 0 : (float)ratingSum / ratingCount;
            overallRating = MathF.Round(overallRating, 2);

            int commentCount = await context.PostComments.Where(c => c.PostId == post.Id).CountAsync();

            var postInfo = new PostInfo
            {
                Id = post.Id,
                Author = user.Name,
                AuthorId = user.Id,
                Title = post.Title,
                Slug = post.Slug,
                OverallRating = overallRating,
                RatingCount = ratingCount,
                CommentCount = commentCount,
                DateCreated = post.DateCreated,
                DateLastEdited = post.DateLastEdited
            };

            return postInfo;
        }

        private static void FormatOverallRating(List<PostInfo> postInfoList) 
            => postInfoList.ForEach(p => p.OverallRating = MathF.Round(p.OverallRating, 2));

        private static IQueryable<PostInfo> ToPostInfoListQuery(ApplicationDBContext context, IQueryable<Post> postsQuery)
        {
            var commentCountsQuery = GetCommentCountsQuery(context);
            var overallRatingsQuery = GetOverallRatingsQuery(context);

            return from post in postsQuery

                   join user in context.Users
                   on post.UserId equals user.Id

                   join commentCount in commentCountsQuery
                   on post.Id equals commentCount.PostId

                   join overallRating in overallRatingsQuery
                   on post.Id equals overallRating.PostId

                   select new PostInfo
                   {
                       Id = post.Id,
                       Author = user.Name,
                       AuthorId = user.Id,
                       Title = post.Title,
                       Slug = post.Slug,
                       OverallRating = overallRating.Value,
                       RatingCount = overallRating.RatingCount,
                       CommentCount = commentCount.Value,
                       DateCreated = post.DateCreated,
                       DateLastEdited = post.DateLastEdited
                   };
        }

        private static IQueryable<CommentCount> GetCommentCountsQuery(ApplicationDBContext context)
        {
            var nonZeroCommentCounts = from comment in context.PostComments
                                       group comment by comment.PostId into g
                                       select new CommentCount { PostId = g.Key, Value = g.Count() };

            var postIdsWithNonZeroCommentCount = nonZeroCommentCounts.Select(cc => cc.PostId);

            var zeroCommentCounts = from post in context.Posts
                                    where !postIdsWithNonZeroCommentCount.Contains(post.Id)
                                    select new CommentCount { PostId = post.Id, Value = 0 };

            return nonZeroCommentCounts.Union(zeroCommentCounts);
        }

        private static IQueryable<OverallRating> GetOverallRatingsQuery(ApplicationDBContext context)
        {
            var nonZeroOverallRatings = from rating in context.PostRatings
                                        group rating by rating.PostId into g
                                        select new OverallRating
                                        {
                                            PostId = g.Key,
                                            RatingCount = g.Count(),
                                            Value = (float)g.Sum(p => p.Value) / g.Count()
                                        };

            var postIdsWithNonZeroOverallRatings = nonZeroOverallRatings.Select(or => or.PostId);

            var zeroOverallRatings = from post in context.Posts
                                     where !postIdsWithNonZeroOverallRatings.Contains(post.Id)
                                     select new OverallRating { PostId = post.Id, RatingCount = 0, Value = 0 };

            return nonZeroOverallRatings.Union(zeroOverallRatings);
        }
    }
}
