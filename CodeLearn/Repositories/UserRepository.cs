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
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _applicationDBContext;
        public UserRepository(IDbContextFactory<ApplicationDBContext> applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void AddUser(User user)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Add(user);
            //context.SaveChanges();
        }

        public async Task<List<User>> GetAllUser()
        {
            using var context = _applicationDBContext.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        public User GetUserById(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.Where(h => h.Id == id).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Update(user);
            //context.SaveChanges();
        }
    }
}
