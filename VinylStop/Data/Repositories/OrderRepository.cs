using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VinylStop.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

 
        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public IEnumerable<Order> OrdersByCustomerId(string id)=> _appDbContext.Orders.Where(o => o.UserId == id).OrderByDescending(d => d.DatePlaced);

        public IEnumerable<OrderDetail> GetOrderLines(IEnumerable<Order> orderHistory)
        {
            List<OrderDetail> orderLines = new List<OrderDetail>();
            foreach (var order in orderHistory)
            {
                orderLines.AddRange(_appDbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).Include(od => od.Album));
            }

            return orderLines;
        }

        public IEnumerable<Order> GetAllOrders() => _appDbContext.Orders;
        public decimal GetTotalSales() => _appDbContext.Orders.Select(o => o.OrderTotal).Sum();

        public void CreateOrder(Order order, string userId)
        {
            order.UserId = userId;

            order.DatePlaced = DateTime.Now;
            _appDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Album = item.Album,
                    Amount = item.Amount,
                    AlbumId = item.Album.AlbumId,
                    OrderId = order.OrderId,
                    Price = item.Album.Price
                };

                order.OrderTotal += item.Album.Price*item.Amount;

                _appDbContext.OrderDetails.Add(orderDetail);
            }


            _appDbContext.SaveChanges();
        }

        public void UpdateStock()
        {
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                item.Album.InStock -= item.Amount;

                _appDbContext.SaveChanges();
            }
        }

    }
}
