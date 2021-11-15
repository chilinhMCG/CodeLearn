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

        List<User> GetUser();
        User GetUserById(string id);

        User GetUserByName(string name);

        string GetNameOfUserById(string id);
        
        void AddUser(User user);
        void UpdateUser(User user);

        
    }
}
