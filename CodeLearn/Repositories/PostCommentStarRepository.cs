using CodeLearn.Data;
using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class PostCommentStarRepository : IPostCommentStarRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        public PostCommentStarRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<PostCommentStar> GetAsync(Guid userId, Guid commentId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.PostCommentStars.FindAsync(userId, commentId);
        }

        public async Task<IList<PostCommentStar>> GetRangeAsync(Guid userId, IEnumerable<Guid> commentIds)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var commentIdsThatUserGaveStar = await context.PostCommentStars
                                                          .Where(cs => cs.UserId == userId && commentIds.Contains(cs.CommentId))
                                                          .Select(cs => cs.CommentId)
                                                          .ToListAsync();

            var commentIdsSet = new HashSet<Guid>(commentIdsThatUserGaveStar);

            var commentStars = new List<PostCommentStar>();
            foreach (Guid commentId in commentIds)
            {
                PostCommentStar commentStar = null;

                if (commentIdsSet.Contains(commentId))
                {
                    commentStar = new PostCommentStar
                    {
                        UserId = userId,
                        CommentId = commentId
                    };
                }

                commentStars.Add(commentStar);
            }

            return commentStars;
        }

        public async Task AddAsync(PostCommentStar commentStar)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.PostCommentStars.AddAsync(commentStar);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(PostCommentStar commentStar)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.PostCommentStars.Remove(commentStar);
            await context.SaveChangesAsync();
        }

    }
}
