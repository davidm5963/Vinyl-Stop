using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using VinylStop.Data.Repositories;
using VinylStop.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MimeKit;
using VinylStop.Services;
using Microsoft.Extensions.Options;

namespace VinylStop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ShoppingCart _shoppingCart;
        private readonly EmailConfiguration _emailConfiguration;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager, IOptions<EmailConfiguration> emailConfiguration)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _emailConfiguration = emailConfiguration.Value;
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var items = _shoppingCart.GetAllShoppingCartItems();

            foreach (var item in items)
            {
                if (!_shoppingCart.IsShoppingCartStockValid(item.Album, item.Amount))
                {
                    ModelState.AddModelError(item.Album.AlbumId.ToString(), "Sorry! There are currently not enough of this item in stock");
                }
            }
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
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

                OrderCompletedEmailAsync(userId, order);

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

        private  async void OrderCompletedEmailAsync(string userId, Order order)
        {
                var user = await _userManager.FindByIdAsync(userId);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vinyl Stop", "davidmoeller115963@gmail.com"));
                message.To.Add(new MailboxAddress(user.Email));

                message.Subject = "Thank you for placing your order!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = @"

                <div style='padding:20px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>
                            <h1 style='text-align: center; font-family: 'Sigmar One', cursive;'>Thank you for buying your records at VinylStop</h1>
                
                Order Placed at: "
                +
                order.DatePlaced
                +
                @"
                <a style = 'background-color: #4CAF50;
                            border: none;
                            color: white;
                            padding: 15px 32px;
                            text-align: center;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 16px;
                            margin: 4px 2px;
                            cursor: pointer;' href='http://localhost:59034/Account/MyAccount'>
                            View My Orders
                </a>
                <a style = 'background-color: #4CAF50;
                            border: none;
                            color: white;
                            padding: 15px 32px;
                            text-align: center;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 16px;
                            margin: 4px 2px;
                            cursor: pointer;' href='http://localhost:59034/Albums'>
                            Buy more albums
                </a>

                "
            };

            message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                        client.Send(message);
                        client.Disconnect(true);
                    }
               
        }
    }
}