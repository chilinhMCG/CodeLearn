using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface ICommentStarRepository
    {
        public Task<CommentStar> GetAsync(Guid userId, Guid commentId);

        public Task<IList<CommentStar>> GetRangeAsync(Guid userId, IEnumerable<Guid> commentIds);
        public Task AddAsync(CommentStar commentStar);
        public Task RemoveAsync(CommentStar commentStar);

    }
}
