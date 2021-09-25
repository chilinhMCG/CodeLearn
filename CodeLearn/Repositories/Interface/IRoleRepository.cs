using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();

        void AddRole(Role role);

        void UpdateRole(Role role);

        void DeleteRole(Role role);
    }
}
