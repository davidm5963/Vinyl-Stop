using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylStop.Data.Interfaces;
using VinylStop.ViewModels;

namespace VinylStop.Controllers
{
    [Authorize(Roles="Admin")]
    public class DashboardController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public DashboardController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel
            {
                Orders = _orderRepository.GetAllOrders()
            };
            return View();
        }
    }
}