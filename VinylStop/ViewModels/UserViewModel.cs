using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Models;

namespace VinylStop.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
