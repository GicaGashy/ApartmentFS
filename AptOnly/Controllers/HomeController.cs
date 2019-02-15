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


        public async Task<IActionResult> Search([Bind("City")] string City, [Bind("m2From")]decimal m2From, [Bind("m2To")] decimal m2To,
            [Bind("rentingOnly")] bool rentingOnly /*[Bind("meTo")] decimal m2To, [Bind("furbished")] bool IsFurbished*/)
        {
            var allApartments = _context.Apartments.Include(a => a.User)
                                    .Include(a => a.Address).ThenInclude(c => c.City)
                                    .Include(a => a.Status)
                                    .Where(a => a.IsActive == true);

            if (City == null && (m2From == 0 || m2To == 0))
            {
                if (rentingOnly == true)
                {
                    return View(allApartments.Where(a => a.IsRenting == rentingOnly));
                }
                else
                {
                    return View(allApartments);
                }
            }

            if (m2From == 0 || m2To == 0)
            {
                if (rentingOnly == true)
                {
                    var CityOnlyFiltered = allApartments.Where(a => a.Address.City.CityName.Contains(City));
                    return View(CityOnlyFiltered.Where(a => a.IsRenting == rentingOnly));
                }
                else
                {
                    var CityOnlyFiltered = allApartments.Where(a => a.Address.City.CityName.Contains(City));
                    return View(CityOnlyFiltered);
                }
            }

            if(City == null && (m2From != 0 && m2To != 0))
            {
                if (rentingOnly == true)
                {
                    var MeterFileterd = allApartments.Where(a => a.M2 >= m2From && a.M2 <= m2To);
                    return View(MeterFileterd.Where(a => a.IsRenting == rentingOnly));
                }
                else
                {
                    var MeterFileterd = allApartments.Where(a => a.M2 >= m2From && a.M2 <= m2To);
                    return View(MeterFileterd);
                }
            }



            var apartmentsFiltered = allApartments.Where(a => a.Address.City.CityName.Contains(City))
                                                .Where(a => a.M2 >= m2From && a.M2 <= m2To);

            if (rentingOnly == true)
            {
                return View(apartmentsFiltered.Where(a => a.IsRenting == rentingOnly));
            }
            else
            {
                return View(apartmentsFiltered);
            }

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
            .Include(a => a.Status).Take(i).Where(a => a.IsActive == true);

            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> List([Bind("tipi")] string i) {

            var apartments = new List<Apartment>();
            var dbApartments = await _context.Apartments
                    .Include(a => a.User)
                    .Include(a => a.Address).ThenInclude(c => c.City)
                    .Include(a => a.Status)
                    .Where(a => a.IsActive == true)
                    .ToListAsync();


            if (i == null || i == "all")
            {
                apartments = dbApartments;
                ViewBag.Type = "Te Gjitha";
            } else if (i == "time") {
                apartments = dbApartments.OrderByDescending(a => a.TimeStamp).ToList();
                 ViewBag.Type = "Sipas Kohes";
            } else if (i == "renting") {
                apartments = dbApartments.Where(a => a.IsRenting == true).ToList();
                ViewBag.Type = "Me Qera";
            }
            else if (i == "selling")
            {
                apartments = dbApartments.Where(a => a.IsRenting == false).ToList();
                ViewBag.Type = "Ne Shitje";
            }
            else if(i == "m2Ascending")
            {
                apartments = dbApartments.OrderBy(a => a.M2).ToList();
                ViewBag.Type = "Metra Katror >";
            }
            else if (i == "m2Descending")
            {
                apartments = dbApartments.OrderByDescending(a => a.M2).ToList();
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
                .Where(a => a.IsActive == true)
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
