using AlbumStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumStore.ViewModels.AccountViewModels
{
    public class MyAccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Order> OrderHistory { get; set; }
        public IEnumerable<OrderDetail> OrderLines { get; set; }

    }
}
