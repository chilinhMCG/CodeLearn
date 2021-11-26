using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IPostRepository
    {
        void AddPost(Post post);

        Post GetPostById(string id);
        Task<ICollection<Post>> GetPostsByAuthor(string userid);
        void UpdatePost(Post post);
        void DeletePostByID(Guid id);
        Task<ICollection<Post>> GetAllPost();
    }
}
