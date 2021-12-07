using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IdentityResult> Add(IdentityRole role);
        Task Delete(IdentityRole role);
        Task Edit(IdentityRole role);
        Task<IEnumerable<IdentityRole>> GetAllRole();
        Task<IdentityRole> FindByName(string name);
        Task<IdentityRole> GetRoleById(string id);
        Task<IList<string>> GetRoleName();

    }
}
