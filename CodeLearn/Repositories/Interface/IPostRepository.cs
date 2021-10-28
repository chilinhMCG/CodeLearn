using CodeLearn.Data.OrderingQuery;
using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<Post> GetAsync(Guid userId, string slug);

        public Task<Page<PostInfo>> GetPagePostInfo(int pageSize, int pageNumber, OrderingQueryDelegate<PostInfo> orderingQuery = null);

        public Task<PostInfo> GetPostInfoAsync(Guid id);

        public Task<Page<PostInfo>> GetPagePostInfoByKeywords(int pageSize, int pageNumber,
            IEnumerable<string> keywords, OrderingQueryDelegate<PostInfo> orderingQuery = null);
    }
}
