using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    public class Apartment : IRentable
    {
        [Required]
        public int ApartmentId { get; set; }
        [Required]
        [RegularExpression(("[0-9]"), ErrorMessage="Only numbers can be entered")]
        public int Floor { get; set; }
        [Required]
        [RegularExpression(("[0-9]"), ErrorMessage = "Only numbers can be entered")]
        public int Bedroom { get; set; }
        [Required]
        [RegularExpression(("[0-9]"), ErrorMessage = "Only numbers can be entered")]
        public int BathRoom { get; set; }
        [Required]
        [RegularExpression(("[0-9][.,]"), ErrorMessage = "Only numbers can be entered")]
        public decimal? PricePerMonth { get; set; }
        public bool? IsFurbished { get; set; }
        [Required]
        [RegularExpression(("[0-9][.,]"), ErrorMessage = "Only numbers can be entered")]
        public decimal? M2 { get; set; }
        [Required]
        [RegularExpression(("[0-9][.,]"), ErrorMessage = "Only numbers can be entered")]
        public decimal? PricePerM2 { get; set; }
        public string Image { get; set; }

        //relationship to address
        public Address Address { get; set; }

        //relationship to status
        public Status Status { get; set; }

        //relatinship to user
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
