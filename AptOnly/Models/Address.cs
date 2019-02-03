using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    public class Address
    {
        [Required]
        public int AddressId { get; set; }
        [Required]
        public City City { get; set; }

        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string VillageName { get; set; }

        //identity
    }
}
