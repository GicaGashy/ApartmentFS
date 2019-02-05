using AptOnly.Data;
using AptOnly.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.ViewModels
{
    public class CreateApartment
    {
        public Apartment Apartment { get; set; }
        public Address Address { get; set; }
        public Status Status { get; set; }
        public List<City>Cities { set; get; }
        public int CityId;
        public string UserId { get; set; }
    }


}
