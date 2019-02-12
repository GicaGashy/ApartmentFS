using AptOnly.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Areas.Admin.ViewModels
{
    public class ApartmentsUsersViewModel
    {
        public ICollection<Apartment> Apartments { get; set; }
        public ICollection<IdentityUser> Users { get; set; }
    }
}
