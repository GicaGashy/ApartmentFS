using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AptOnly.Areas.Admin.ViewModels;
using AptOnly.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AptOnly.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> rolesManagers;

         public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Users UsersViewModel = new Users();

            UsersViewModel.users = _userManager.GetUsersInRoleAsync("GenericUser").Result;
            return View(UsersViewModel);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid && _userManager.FindByEmailAsync(user.Email).Result == null)
            {
                var newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };
                IdentityResult result = _userManager.CreateAsync(newUser, user.Password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(newUser,"GenericUser").Wait();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}