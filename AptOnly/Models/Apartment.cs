using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    public class Apartment
    {
        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Bedroom { get; set; }

        [Required]
        public int BathRoom { get; set; }

        [Required]
        public decimal PricePerMonth { get; set; }


        public bool IsFurbished { get; set; }

        [Required]
        public decimal M2 { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal PricePerM2 { get; set; }

        public string Image { get; set; }

        //relationship to address
        public virtual Address Address { get; set; }

        //relationship to status
        public virtual Status Status { get; set; }

        //relatinship to user
        public IdentityUser User { get; set; }
        public string UserId { get; set; }

        public Apartment() {
            Image = "apt.png";
        }

        public decimal GetTotalPrice()
        {
            return M2 * PricePerM2;
        }
    }
}
