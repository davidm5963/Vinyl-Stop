using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbumStore.Data.Interfaces;
using AlbumStore.Data.Models;
using AlbumStore.Data.Repositories;
using AlbumStore.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MimeKit;

namespace AlbumStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        [Authorize]
        public ViewResult Checkout()
        {
            ModelState.AddModelError("LoginRequired", "Please log in before checking out");

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var items = _shoppingCart.GetAllShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if(_shoppingCart.ShoppingCartItems.Count <= 0)
            {
                ModelState.AddModelError("EmptyCart", "Your cart is empty. Add some albums first");
            }
            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order, userId);
                _orderRepository.UpdateStock();
                _shoppingCart.ClearCart();

                RedirectToAction("OrderConfirmationEmailAsync", "Order", userId);
                return RedirectToAction("CheckoutComplete");
            }
            else
            {
                return View(order);

            }


        }

        public ViewResult CheckoutComplete()
        {
            ViewBag.Message = "Thank you for placing your order! :)";
            return View();
        }

        public async void OrderConfirmationEmailAsync(string userId)
        {
                var user = await _userManager.FindByIdAsync(userId);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("AlbumAndGoHelp@gmail.com"));
                message.To.Add(new MailboxAddress(user.Email));

                message.Subject = "Order Details";
                message.Body = new TextPart("html")
                {
                    Text = @"Thank you for placing your order at AlbumAndGo!"
                };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587);


                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("AlbumAndGoHelp@gmail.com", "Password5963");

                        client.Send(message);
                        client.Disconnect(true);
                    }
               
        }
    }
}