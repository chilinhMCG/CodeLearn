using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IPostCommentStarRepository
    {
        public Task<PostCommentStar> GetAsync(Guid userId, Guid commentId);

        public Task<IList<PostCommentStar>> GetRangeAsync(Guid userId, IEnumerable<Guid> commentIds);
        public Task AddAsync(PostCommentStar commentStar);
        public Task RemoveAsync(PostCommentStar commentStar);

    }
}
