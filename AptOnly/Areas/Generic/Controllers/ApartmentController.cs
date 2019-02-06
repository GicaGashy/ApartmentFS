using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AptOnly.Data;
using AptOnly.Models;
using AptOnly.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AptOnly.Areas.Generic.Controllers
{
    [Area("Generic")]
    public class ApartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Generic/Apartments
        [Authorize]
        [Area("Generic")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Apartments.Include(apartment => apartment.User)
                .Include(a => a.Address).ThenInclude(c => c.City)
                .Include(a => a.Status).Where(u => u.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await applicationDbContext.ToListAsync());
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
                .Include(a => a.Status).Where(u => u.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        //Generic/Apartment/Create
        [Authorize]
        [Area("Generic")]
        public IActionResult Create()
        {
            var vm = new CreateApartment();
            vm.Cities = _context.Cities.ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Area("Generic")]
        public async Task<IActionResult> Create(
            [Bind("AddressId,StreetName1,StreetName2,VillageName, City")] Address address,
            [Bind("StatusId, Description, IsNew, ReleaseDate")] Status status,
            [Bind("ApartmentId, Floor, Bedroom, BathRoom, PricePerMonth, IsFurbished, M2, PricePerM2, Image, Address, Status, UserId")] Apartment apartment,
            City city, IFormFile imageFile){
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vm = new CreateApartment();
            address.City = city;
            apartment.Address = address;
            apartment.Status = status;
            apartment.UserId = userId;
            vm.CityId = city.CityId;
            vm.Address = address;
            vm.Status = status;
            vm.Apartment = apartment;
            vm.Cities = _context.Cities.ToList();
            if (ModelState.IsValid) {

                #region
                if (imageFile != null)
                {
                    //Image loading part
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);
                    var fileDirectory = "wwwroot/images";
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    var filePath = fileDirectory + "/" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    apartment.Image = fileName;
                    //Image loading part end
                }
                else {
                    apartment.Image = "def.png";
                }
                #endregion
                _context.Add(address);
                _context.Add(status);
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return View(vm);
            }
            return View(vm);
        }

    }
}