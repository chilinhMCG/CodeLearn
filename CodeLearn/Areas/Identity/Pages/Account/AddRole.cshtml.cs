using CodeLearn.Data;
using CodeLearn.Models;
using CodeLearn.Repositories;
using CodeLearn.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace CodeLearn.Areas.Identity.Pages.Account
{
    public class AddRoleModel : PageModel
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IRoleRepository _roleRepository;

        [Inject]
        private ISnackbar _snackbar { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public AddRoleModel(RoleManager<IdentityRole> roleManager, IRoleRepository roleRepository)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }


        public class InputModel
        {
            [Required]
            [Display(Name = "Role ID")]
            public string RoleID { get; set; }


            [Required]
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
        }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Id = Input.RoleID, Name = Input.RoleName };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    AddRole();
                    _snackbar.Add("Thêm thành công", Severity.Success);
                    return Redirect("/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }


        public void AddRole()
        {
            Role newRole = new Role { RoleID = Input.RoleID, RoleName = Input.RoleName };

            _roleRepository.AddRole(newRole);
        }
    }
}
