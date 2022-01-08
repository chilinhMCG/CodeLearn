using CodeLearn.Data.OrderingQuery;
using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    interface IPostCommentRepository : IRepository<PostComment>
    {
        public Task<PostCommentInfo> GetCommentInfoAsync(Guid commentId);

        public Task<Page<PostCommentInfo>> GetPageTopLevelCommentInfoAsync(
            int pageSize, int pageNumber, Guid postId, OrderingQueryDelegate<PostCommentInfo> orderingQuery = null);

        public Task<Page<PostCommentInfo>> GetPageReplyInfoAsync(
            int pageSize, int pageNumber, Guid commentId, OrderingQueryDelegate<PostCommentInfo> orderingQuery = null);
    }
}
