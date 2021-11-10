using CodeLearn.Data.OrderingQuery;
using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    interface ICommentRepository : IRepository<Comment>
    {
        public Task<CommentInfo> GetCommentInfoAsync(Guid commentId);

        public Task<Page<CommentInfo>> GetPageTopLevelCommentInfoAsync(
            int pageSize, int pageNumber, Guid postId, OrderingQueryDelegate<CommentInfo> orderingQuery = null);

        public Task<Page<CommentInfo>> GetPageReplyInfoAsync(
            int pageSize, int pageNumber, Guid commentId, OrderingQueryDelegate<CommentInfo> orderingQuery = null);
    }
}
