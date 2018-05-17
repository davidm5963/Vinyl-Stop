using VinylStop.Data.Models;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Components
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart ShoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = ShoppingCart.GetAllShoppingCartItems();
            ShoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = this.ShoppingCart,
                Total = ShoppingCart.GetCartTotal()
            };

            return View(shoppingCartViewModel);

        }
    }
}
