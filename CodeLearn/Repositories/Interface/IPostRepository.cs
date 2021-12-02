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

        public Task<Page<PostInfo>> GetPagePostInfoAsync(int pageSize, int pageNumber, OrderingQueryDelegate<PostInfo> orderingQuery = null);

        public Task<PostInfo> GetPostInfoAsync(Guid id);

        public Task<Page<PostInfo>> GetPagePostInfoSearchByKeywordsAsync(int pageSize, int pageNumber,
            string keywordsText, OrderingQueryDelegate<PostInfo> orderingQuery = null);

        public Task<Page<PostInfo>> GetPagePostInfoSearchByAuthorNameAsync(int pageSize, int pageNumber,
            string authorName, OrderingQueryDelegate<PostInfo> orderQuery = null);
    }
}
