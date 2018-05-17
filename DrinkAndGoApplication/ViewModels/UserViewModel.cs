using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
