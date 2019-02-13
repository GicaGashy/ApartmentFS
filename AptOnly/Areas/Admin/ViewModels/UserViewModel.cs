using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Emri i Perdoruesit")]
        public string UserName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefon valid")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} duhet te jete se paku {2} dhe me se shumti {1} karaktere", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Fjalkalimi nuk pershtatet")]
        public string ConfirmPassword { get; set; }

        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
