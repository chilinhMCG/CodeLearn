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
    public class PostRatingRepository : IPostRatingRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        public PostRatingRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<PostRating> GetAsync(Guid userId, Guid postId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var postRating = await context.PostRatings.FindAsync(userId, postId);
            return postRating ?? new PostRating() { UserId = userId, PostId = postId, Value = 0 };
        }

        public async Task<IList<PostRating>> GetRangeAsync(Guid userId, IEnumerable<Guid> postIds)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var postRatings = new List<PostRating>(postIds.Count());

            foreach (Guid postId in postIds)
            {
                var rating = await context.PostRatings.FindAsync(userId, postId);

                if (rating == null)
                {
                    rating = new PostRating()
                    {
                        PostId = postId,
                        UserId = userId,
                        Value = 0,
                    };
                }

                postRatings.Add(rating);
            }

            return postRatings;
        }

        public async Task AddAsync(PostRating postRating)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.PostRatings.AddAsync(postRating);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostRating updatedPostRating)
        {
            using var context = _dbContextFactory.CreateDbContext();
            PostRating postRating = await context.PostRatings.FindAsync(updatedPostRating.UserId, updatedPostRating.PostId);

            postRating.Value = updatedPostRating.Value;
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(PostRating postRating)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.PostRatings.Attach(postRating);
            context.PostRatings.Remove(postRating);

            await context.SaveChangesAsync();
        }
    }
}
