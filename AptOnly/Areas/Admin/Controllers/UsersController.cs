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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var vm = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var originalUser = await _userManager.FindByIdAsync(user.Id);

                if (originalUser == null)
                {
                    return NotFound();
                }

                if (originalUser.Email != user.Email) // Have we changed the email?
                {
                    // Yes, check if unique...
                    if (_userManager.FindByEmailAsync(user.Email).Result == null)
                    {
                        originalUser.Email = user.Email;
                        originalUser.UserName = user.Email;
                        originalUser.PhoneNumber = user.PhoneNumber;
                        await _userManager.UpdateAsync(originalUser);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email already exists. Please try another one.");

                        return View();
                    }
                }else if(originalUser.PhoneNumber != user.PhoneNumber)
                {
                    if (_userManager.FindByEmailAsync(user.Email).Result != null)
                    {
                        originalUser.PhoneNumber = user.PhoneNumber;
                        await _userManager.UpdateAsync(originalUser);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Phone couldn't be changed or some other shit");
                        return View();
                    }
                }

                if (!string.IsNullOrEmpty(user.Password)) // Are we trying to set a new password?
                {
                    // Yes, update it

                    string code = await _userManager.GeneratePasswordResetTokenAsync(originalUser);
                    var result = await _userManager.ResetPasswordAsync(originalUser, code, user.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }

                        return View();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}