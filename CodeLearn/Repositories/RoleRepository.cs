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
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _dbContextFactory;

        public RoleRepository(IDbContextFactory<ApplicationDBContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public List<Role> GetRoles()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Roles.ToList();
        }

        public void AddRole(Role role)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Roles.Add(role);
            context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Roles.Update(role);
            context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Roles.Remove(role);
            context.SaveChanges();
        }
    }
}
