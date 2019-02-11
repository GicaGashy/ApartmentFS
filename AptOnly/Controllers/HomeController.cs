using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AptOnly.Models;
using AptOnly.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AptOnly.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Search([Bind("City")] string City, [Bind("meFrom")]decimal m2From, [Bind("meTo")] decimal m2To
            /*[Bind("meFrom")]decimal m2From, [Bind("meTo")] decimal m2To, [Bind("furbished")] bool IsFurbished*/)
        {
            var allApartments = _context.Apartments.Include(a => a.User)
                                    .Include(a => a.Address).ThenInclude(c => c.City)
                                    .Include(a => a.Status);
            if (City == null && (m2From == 0 || m2To == 0))
            {
                return View(allApartments);
            }

            if (m2From == 0 || m2To == 0)
            {
                var CityOnlyFiltered = allApartments.Where(a => a.Address.City.CityName.Contains(City));
                return View(CityOnlyFiltered);
            }

            if(City == null && (m2From != 0 && m2To != 0))
            {
                var MeterFileterd = allApartments.Where(a => a.M2 >= m2From && a.M2 <= m2To);
                return View(MeterFileterd);
            }



            var apartmentsFiltered = allApartments.Where(a => a.Address.City.CityName.Contains(City))
                                                .Where(a => a.M2 >= m2From && a.M2 <= m2To);


            return View(apartmentsFiltered);
        }

        public async Task<IActionResult> About()
        {
            var apartments = await _context.Apartments.ToListAsync();

            return View(apartments);
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


        public async Task<IActionResult> List([Bind("tipi")] string i) {

            var apartments = new List<Apartment>();

            if (i == null || i == "all")
            {
                apartments = await _context.Apartments
                    .Include(a => a.User)
                    .Include(a => a.Address).ThenInclude(c => c.City)
                    .Include(a => a.Status)
                    .ToListAsync();
                ViewBag.Type = "Te Gjitha";
            } else if (i == "time") {
                apartments = await _context.Apartments
                    .Include(a => a.User)
                    .Include(a => a.Address).ThenInclude(c => c.City)
                    .Include(a => a.Status).OrderByDescending(a => a.TimeStamp)
                    .ToListAsync();
                 ViewBag.Type = "Sipas Kohes";
            } else if (i == "renting") {
                apartments = await _context.Apartments
                .Include(a => a.User)
                .Include(a => a.Address).ThenInclude(c => c.City)
                .Include(a => a.Status).Where(a => a.IsRenting == true)
                .ToListAsync();
                ViewBag.Type = "Me Qera";
            }
            else if (i == "selling")
            {
                apartments = await _context.Apartments
                .Include(a => a.User)
                .Include(a => a.Address).ThenInclude(c => c.City)
                .Include(a => a.Status).Where(a => a.IsRenting == false)
                .ToListAsync();
                ViewBag.Type = "Ne Shitje";
            }
            else if(i == "m2Ascending")
            {
                apartments = await _context.Apartments
                    .Include(a => a.User)
                    .Include(a => a.Address).ThenInclude(c => c.City)
                    .Include(a => a.Status).OrderBy(a => a.M2)
                    .ToListAsync();
                ViewBag.Type = "Metra Katror >";
            }
            else if (i == "m2Descending")
            {
                apartments = await _context.Apartments
                    .Include(a => a.User)
                    .Include(a => a.Address).ThenInclude(c => c.City)
                    .Include(a => a.Status).OrderByDescending(a => a.M2)
                    .ToListAsync();
                ViewBag.Type = "Metra Katror <";
            }

            return View(apartments);
        }



        // GET: Generic/Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var apartment = await _context.Apartments
                .Include(a => a.User)
                .Include(a => a.Address).ThenInclude(c => c.City)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
