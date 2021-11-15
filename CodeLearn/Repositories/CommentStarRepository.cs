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
    public class CommentStarRepository : ICommentStarRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        public CommentStarRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<CommentStar> GetAsync(Guid userId, Guid commentId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.CommentStars.FindAsync(userId, commentId);
        }

        public async Task<IList<CommentStar>> GetRangeAsync(Guid userId, IEnumerable<Guid> commentIds)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var commentIdsThatUserGaveStar = await context.CommentStars
                                                          .Where(cs => cs.UserId == userId && commentIds.Contains(cs.CommentId))
                                                          .Select(cs => cs.CommentId)
                                                          .ToListAsync();

            var commentIdsSet = new HashSet<Guid>(commentIdsThatUserGaveStar);

            var commentStars = new List<CommentStar>();
            foreach (Guid commentId in commentIds)
            {
                CommentStar commentStar = null;

                if (commentIdsSet.Contains(commentId))
                {
                    commentStar = new CommentStar
                    {
                        UserId = userId,
                        CommentId = commentId
                    };
                }

                commentStars.Add(commentStar);
            }

            return commentStars;
        }

        public async Task AddAsync(CommentStar commentStar)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.CommentStars.AddAsync(commentStar);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(CommentStar commentStar)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.CommentStars.Remove(commentStar);
            await context.SaveChangesAsync();
        }

    }
}
