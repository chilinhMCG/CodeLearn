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
            List<Discussion> discussions = context.Discussions.ToList().Where(t => CheckContain(t, search)).OrderByDescending(t => t.CreateAt).Skip(pageSize * (pageNumbers - 1)).Take(pageSize).ToList();
            return discussions;
        }
        public int GetPageNumbers(int sizePage, string search)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return (context.Discussions.ToList().Where(t => CheckContain(t, search)).Count()/sizePage) + 1;
        }
        private bool CheckContain(Discussion discussion, string search)
        {
            if (discussion.Question.Contains(search) == true)
            {
                return true;
            }
            foreach (var n in discussion.HashTag.ToList())
            {
                if (n.Contains(search) == true)
                {
                    return true;
                }
            }
            return false;
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

        public void UpdateDiscussion(Discussion discussion)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Update(discussion);
            //context.SaveChanges();
        }
    }
}