using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AptOnly.Models;
using AptOnly.Data;
using Microsoft.EntityFrameworkCore;

namespace AptOnly.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index([Bind("Count")] int i)
        {
            if (i < 1)
            {
                i = 3;
            }
            else
            {
                i += 3;
            }

            var applicationDbContext = _context.Apartments.Include(apartment => apartment.User)
            .Include(a => a.Address).ThenInclude(c => c.City)
            .Include(a => a.Status).Take(i);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> List(string type) {
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
