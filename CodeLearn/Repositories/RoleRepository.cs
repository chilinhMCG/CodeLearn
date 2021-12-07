using CodeLearn.Data;
using CodeLearn.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<ApplicationDBContext> _context;
        private readonly RoleManager<IdentityRole> _roleManager; 
        public RoleRepository(RoleManager<IdentityRole> roleManager,IDbContextFactory<ApplicationDBContext> context)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> Add(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result; 
        }

        public async Task Delete(IdentityRole role)
        {
            await _roleManager.DeleteAsync(role);
        }

        public async Task Edit(IdentityRole role)
        {
            await _roleManager.UpdateAsync(role);
            
        }

        public Task<IdentityRole> FindByName(string name)
        {
            var role = _roleManager.FindByNameAsync(name);
            return role; 
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRole()
        {
            var listRole = await _roleManager.Roles.ToListAsync();
            return listRole;
        }

        public async Task<IdentityRole> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IList<string>> GetRoleName()
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }
    }
}
