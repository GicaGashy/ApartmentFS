using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    public class Status
    {
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string Description { get; set; }

        public bool IsNew { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
