using CodeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser();
        User GetUserById(Guid id);
        User GetUserByName(string name);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
