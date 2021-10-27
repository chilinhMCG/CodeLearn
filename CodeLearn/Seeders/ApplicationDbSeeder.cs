using CodeLearn.Models;
using CodeLearn.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Seeders
{
    public class ApplicationDbSeeder : IApplicationDbSeeder
    {
        private IUserRepository _userRepository;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ApplicationDbSeeder(IUserRepository userRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedPostManagerData()
        {
            if (await _roleManager.RoleExistsAsync("Admin") == false)
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await _userManager.FindByEmailAsync("seed_user1@gmail.com") != null)
                return;

            var user1 = new IdentityUser { UserName = "seed_user1", Email = "seed_user1@gmail.com", EmailConfirmed = true };
            await _userManager.CreateAsync(user1, "seed_user1");

            var user2 = new IdentityUser { UserName = "seed_user2", Email = "seed_user2@gmail.com", EmailConfirmed = true };
            await _userManager.CreateAsync(user2, "seed_user2");

            var admin = new IdentityUser { UserName = "seed_admin", Email = "seed_admin@gmail.com", EmailConfirmed = true };
            await _userManager.CreateAsync(admin, "seed_admin");

            await _userManager.AddToRoleAsync(admin, "Admin");

            _userRepository.AddUser(new User
            {
                Name = user1.UserName,
                Email = user1.Email,
                DateJoined = DateTime.Now,
                IsBlocked = false,
                Bio = "seed user 1 bio, hello!",
            });

            _userRepository.AddUser(new User
            {
                Name = user2.UserName,
                Email = user2.Email,
                DateJoined = DateTime.Now,
                IsBlocked = false,
                Bio = "seed user 2 bio, hi there!",
            });

            _userRepository.AddUser(new User
            {
                Name = admin.UserName,
                Email = admin.Email,
                DateJoined = DateTime.ParseExact("2009-09-09", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                IsBlocked = false,
                Bio = "seed admin bio, get banned!",
            });

        }
    }
}
