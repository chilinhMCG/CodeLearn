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
            context.SaveChanges();
        }

        public async Task<List<User>> GetAllUser()
        {
            using var context = _applicationDBContext.CreateDbContext();
            return await context.Users.OrderBy(u => u.Name).ToListAsync();
        }

        public List<User> GetUser()
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.ToList();
        }

        public User GetUserById(string id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.Where(h => h.Id.ToString() == id).FirstOrDefault();
            
        }

        public string GetNameOfUserById(string id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.Where(h => h.Id.ToString() == id).FirstOrDefault().Name;
        }
        public User GetUserByName(string name)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.Where(h => h.Name == name).FirstOrDefault();
        }
        public void UpdateUser(User user)
        {
            using var context = _applicationDBContext.CreateDbContext();
            context.Update(user);
            context.SaveChanges();
        }

        public User GetUserById(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return context.Users.Where(h => h.Id == id).FirstOrDefault();
        }

                public async Task<User> GetUserByEmailAsync(string email)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            using var context = _applicationDBContext.CreateDbContext();
            return await context.Users.FindAsync(id);
        }
    }
}