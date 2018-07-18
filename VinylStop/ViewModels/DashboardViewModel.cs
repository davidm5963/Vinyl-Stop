
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Models;

namespace VinylStop.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
