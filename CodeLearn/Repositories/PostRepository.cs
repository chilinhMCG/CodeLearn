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
    public class PostRepository : IPostRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDBContext;
        public PostRepository(IDbContextFactory<ApplicationDBContext> applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public void AddPost(Post post)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Add(post);
            context.SaveChanges();
        }

        public Post GetPostById(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Posts.Where(h => h.Id == id).FirstOrDefault();
        }

        public Post GetPostById(string id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Posts.Where(h => h.Id.ToString() == id).FirstOrDefault();
        }

        public void UpdatePost(Post post)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Update(post);
            context.SaveChanges();
        }

        public void DeletePostByID(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            Post post = GetPostById(id);
            context.Remove(post);
            context.SaveChanges();
        }

        public async Task<ICollection<Post>> GetAllPost()
        {
            using var context = _applicationDBContext.CreateDbContext();
            var list = await context.Posts.ToListAsync();
            return list;
        }
    }
}
