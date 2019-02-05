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
    public class ApartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        public async Task<IActionResult> Create(
            [Bind("AddressId,StreetName1,StreetName2,VillageName")] Address address,
            [Bind("StatusId, Description, IsNew, ReleaseDate")] Status status,
            [Bind("ApartmentId, Floor, Bedroom, BathRoom, PricePerMonth, IsFurbished, M2, PricePerM2, Image, Address, Status, IdentityUser")]Apartment apartment
            )
        {

            return View(CreateApartment);
        }
        */
        //Generic/Apartment/Create
        [Authorize]
        [Area("Generic")]
        public IActionResult Create()
        {
            //ViewData["City"] = new SelectList(_context.Cities, "City", "City");
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
            City city){

            #region
            /*
            //Image loading part
            var fileName = Path.GetRandomFileName() + Path.GetExtension(image.FileName);
            var fileDirectory = "wwwroot/images";

            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            var filePath = fileDirectory + "/" + fileName;

            // Copy file to path...
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            apartment.Image = fileName;
            //Image loading part end
            */
            //ViewData["City"] = new SelectList(_context.Cities, "CityId", "CityId", vm.Cities);
            #endregion

            //ICollection<City> cities = _context.Cities.ToList();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var vm = new CreateApartment();
            address.City = city;
            apartment.Address = address;
            apartment.Status = status;
            apartment.Image = "def.png";
            apartment.UserId = userId;

            vm.CityId = city.CityId;
            vm.Address = address;
            vm.Status = status;
            vm.Apartment = apartment;

            vm.Cities = _context.Cities.ToList();

            if (ModelState.IsValid) {             
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