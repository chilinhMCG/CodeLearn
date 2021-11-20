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
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDBContext;
        public DiscussionRepository(IDbContextFactory<ApplicationDBContext> applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void AddDiscussion(Discussion discussion)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Add(discussion);
            context.SaveChanges();
        }
        public void DeleteDiscussionByID(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            Discussion discussion = GetDiscussionById(id);
            context.Remove(discussion);
            context.SaveChanges();
        }
        public List<Discussion> GetDiscussionPage(int pageNumbers, int pageSize, string search)
        {
            using var context = _applicationDBContext.CreateDbContext();
            List<Discussion> discussions = context.Discussions.Include(s => s.User).Where(t=>t.Question.Contains(search) || t.HashTag.Contains(search)).OrderByDescending(t => t.CreateAt).Skip(pageSize * (pageNumbers - 1)).Take(pageSize).ToList();
            return discussions;
        }
        public int GetPageNumbers(int sizePage, string search)
        {
            using var context = _applicationDBContext.CreateDbContext();
            int count = context.Discussions.Where(t => t.Question.Contains(search) || t.HashTag.Contains(search)).Count();
            if (count % sizePage == 0) return (count / sizePage);
            else return (count / sizePage) + 1;
        }
        public async Task<List<User>> GetAllUser()
        {
            using var context = _applicationDBContext.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        public Discussion GetDiscussionById(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Discussions.Where(h => h.Id == id).FirstOrDefault();
        }
        public Discussion GetDiscussionById(string id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Discussions.Where(h => h.Id.ToString() == id).FirstOrDefault();
        }
        public async Task<ICollection<Discussion>> GetDiscussionByAuthor(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            var result = await (from d in context.Discussions
                                where d.UserId == id
                                select d).ToListAsync(); ;
            return result;
        }

        public void UpdateDiscussion(Discussion discussion)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Update(discussion);
            //context.SaveChanges();
        }

        public async Task<ICollection<Discussion>> GetAllDiscussion()
        {
            using var context = _applicationDBContext.CreateDbContext();
            var list = await context.Discussions.ToListAsync();
            return list; 
            //context.SaveChanges();
        }
    }
}