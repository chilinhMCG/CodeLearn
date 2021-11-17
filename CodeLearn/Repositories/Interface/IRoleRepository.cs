using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IdentityRole> Add(IdentityRole role);
        Task<IdentityRole> Delete(IdentityRole role);
        Task<IdentityRole> Edit(IdentityRole role);

    }
}
