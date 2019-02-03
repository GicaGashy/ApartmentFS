using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AptOnly.Models
{
    interface IRentable
    {
        decimal? PricePerMonth { get; set; }
        bool? IsFurbished { get; set; }

    }
}
