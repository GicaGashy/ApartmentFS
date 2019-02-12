using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AptOnly.Areas.Admin.ViewModels;
using AptOnly.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AptOnly.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        [Area("Admin")]
        public IActionResult Index()
        {
            
            var vm = new ApartmentsUsersViewModel();
            vm.Users = _userManager.GetUsersInRoleAsync("GenericUser").Result;
            vm.Apartments = _context.Apartments.ToList();

            return View(vm);
        }
    }
}