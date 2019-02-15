using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AptOnly.Models;

namespace AptOnly.ViewModels
{
    public class IndexViewModel
    {
        public PaginatedList<Apartment> Apartments { get; set; }
    }
}
