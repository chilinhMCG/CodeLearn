using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IPostRatingRepository
    {
        /// <summary>
        /// <para>If user has not rate the post, then that post rating value will be 0.</para> 
        /// </summary>
        public Task<PostRating> GetAsync(Guid userId, Guid postId);

        /// <summary>
        /// <para>Get a range of post ratings of a given user.</para> 
        /// <para>If user has not rate a post, then that post rating value will be 0.</para> 
        /// </summary>
        public Task<IList<PostRating>> GetRangeAsync(Guid userId, IEnumerable<Guid> postIds);

        public Task AddAsync(PostRating postRating);

        public Task UpdateAsync(PostRating postRating);

        public Task RemoveAsync(PostRating postRating);
    }
}
