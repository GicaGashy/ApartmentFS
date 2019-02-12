using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Areas.Admin.ViewModels
{
    public class Users
    {
        public ICollection<IdentityUser> users { get; set; }
    }
}
