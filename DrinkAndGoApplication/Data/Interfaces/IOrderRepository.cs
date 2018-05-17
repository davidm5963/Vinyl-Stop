using VinylStop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Data.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order, string userId);

        IEnumerable<Order> OrdersByCustomerId(string userId);

        IEnumerable<OrderDetail> GetOrderLines(IEnumerable<Order> orders);

        void UpdateStock();
    }
}
