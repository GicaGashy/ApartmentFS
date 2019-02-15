using AptOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.ViewModels
{
    public class CitiesApartment
    {
        public IEnumerable<Apartment> Aparttments { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
