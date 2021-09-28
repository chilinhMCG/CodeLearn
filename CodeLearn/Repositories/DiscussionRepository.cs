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

        public List<Discussion> GetAllDiscussionType()
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Discussions.ToList();
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