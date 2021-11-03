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
        void AddUser(User user);
        void UpdateUser(User user);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(Guid id);
    }
}
