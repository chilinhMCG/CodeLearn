using CodeLearn.Data;
using CodeLearn.Data.OrderingQuery;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
    {
        private class PropCount
        {
            public Guid CommentId { get; set; }
            public int Value { get; set; }
        }

        public PostCommentRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        /// <summary>
        /// clientQuery can be used for ordering, filtering items before the pagination process
        /// </summary>
        public async Task<Page<PostCommentInfo>> GetPageTopLevelCommentInfoAsync(
            int pageSize, int pageNumber, Guid postId, OrderingQueryDelegate<PostCommentInfo> orderingQuery = null)
        {
            using var context = DbContextFactory.CreateDbContext();

            var tlcommentsQuery = context.PostComments.Where(c => c.PostId == postId && c.ParentCommentId == null)
                                                .Include(c => c.User);

            int pageCount = PaginationUtils.GetPageCount(pageSize, await tlcommentsQuery.CountAsync());

            if (pageNumber > pageCount)
            {
                return new Page<PostCommentInfo>()
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = pageCount
                };
            }

            var starCountsQuery = GetStarCountsQuery(context);
            var replyCountsQuery = GetReplyCountsQuery(context);

            var commentInfoListQuery = (from comment in tlcommentsQuery

                                        join starCount in starCountsQuery
                                             on comment.Id equals starCount.CommentId

                                        join replyCount in replyCountsQuery
                                             on comment.Id equals replyCount.CommentId

                                        select new PostCommentInfo
                                        {
                                            Comment = comment,
                                            UserName = comment.User.Name,
                                            StarCount = starCount.Value,
                                            ReplyCount = replyCount.Value,
                                        });


            if (orderingQuery != null)
            {
                commentInfoListQuery = orderingQuery(new OrderingQuery<PostCommentInfo>(commentInfoListQuery))
                                       .GetInnerQuery();
            }

            var commentInfoList = await commentInfoListQuery.TakeFromPage(pageNumber, pageSize)
                                                            .ToListAsync();

            return new Page<PostCommentInfo>
            {
                Items = commentInfoList,
                Size = pageSize,
                CurrentPage = pageNumber,
                PageCount = pageCount,
            };
        }

        private static IQueryable<PropCount> GetStarCountsQuery(ApplicationDBContext context)
        {
            var nonZerostarCounts = from commentStar in context.PostCommentStars
                                    group commentStar by commentStar.CommentId into g
                                    select new PropCount { CommentId = g.Key, Value = g.Count() };

            var commentIdsWithNonZeroStarCount = nonZerostarCounts.Select(sc => sc.CommentId);

            var zeroStarCounts = from comment in context.PostComments
                                 where !commentIdsWithNonZeroStarCount.Contains(comment.Id)
                                 select new PropCount { CommentId = comment.Id, Value = 0 };

            return nonZerostarCounts.Union(zeroStarCounts);
        }

        private static IQueryable<PropCount> GetReplyCountsQuery(ApplicationDBContext context)
        {
            var nonZeroReplyCounts = from comment in context.PostComments
                                     group comment by comment.ParentCommentId into g
                                     where g.Key != null
                                     select new PropCount { CommentId = g.Key ?? default, Value = g.Count() };

            var commentIdsWithNonZeroReplies = nonZeroReplyCounts.Select(sc => sc.CommentId);

            var zeroReplyCounts = from comment in context.PostComments
                                  where !commentIdsWithNonZeroReplies.Contains(comment.Id)
                                  select new PropCount { CommentId = comment.Id, Value = 0 };

            return nonZeroReplyCounts.Union(zeroReplyCounts);
        }

        public async Task<Page<PostCommentInfo>> GetPageReplyInfoAsync(
            int pageSize, int pageNumber, Guid commentId, OrderingQueryDelegate<PostCommentInfo> orderingQuery = null)
        {
            using var context = DbContextFactory.CreateDbContext();

            var repliesQuery = context.PostComments.Where(c => c.ParentCommentId == commentId)
                                               .Include(c => c.User);

            int pageCount = PaginationUtils.GetPageCount(pageSize, await repliesQuery.CountAsync());

            if (pageNumber > pageCount)
            {
                return new Page<PostCommentInfo>()
                {
                    Size = pageSize,
                    CurrentPage = pageNumber,
                    PageCount = pageCount
                };
            }

            var starCountsQuery = GetStarCountsQuery(context);
            var replyCountsQuery = GetReplyCountsQuery(context);

            var repliesInfoListQuery = (from reply in repliesQuery

                                        join starCount in starCountsQuery
                                             on reply.Id equals starCount.CommentId

                                        join replyCount in replyCountsQuery
                                             on reply.Id equals replyCount.CommentId

                                        orderby reply.DateCreated ascending
                                        select new PostCommentInfo
                                        {
                                            Comment = reply,
                                            UserName = reply.User.Name,
                                            StarCount = starCount.Value,
                                            ReplyCount = replyCount.Value,
                                        });

            if (orderingQuery != null)
            {
                repliesInfoListQuery = orderingQuery(new OrderingQuery<PostCommentInfo>(repliesInfoListQuery))
                                       .GetInnerQuery();
            }

            var repliesInfoList = await repliesInfoListQuery.TakeFromPage(pageNumber, pageSize)
                                                            .ToListAsync();

            return new Page<PostCommentInfo>
            {
                Items = repliesInfoList,
                Size = pageSize,
                CurrentPage = pageNumber,
                PageCount = pageCount,
            };
        }

        public async Task<PostCommentInfo> GetCommentInfoAsync(Guid commentId)
        {
            using var context = DbContextFactory.CreateDbContext();

            var commentQuery = context.PostComments.Where(c => c.Id == commentId)
                                               .Include(c => c.User);


            var starCountQuery = context.PostCommentStars.Where(cs => cs.CommentId == commentId);

            var replyCountQuery = context.PostComments.Where(c => c.ParentCommentId == commentId);

            var commentInfoQuery = commentQuery.Select(c => new PostCommentInfo
            {
                Comment = c,
                UserName = c.User.Name,
                StarCount = starCountQuery.Count(),
                ReplyCount = replyCountQuery.Count(),
            });

            return await commentInfoQuery.FirstOrDefaultAsync();
        }
    }
}
