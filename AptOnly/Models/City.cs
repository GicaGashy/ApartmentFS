using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    public class City
    {   
        [Required]
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
