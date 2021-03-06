﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylStop.Data.Interfaces;
using VinylStop.Data.Models;
using VinylStop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace VinylStop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ShoppingCart ShoppingCart;

        public ShoppingCartController(IAlbumRepository albumRepository, ShoppingCart shoppingCart)
        {
            _albumRepository = albumRepository;
            ShoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = ShoppingCart.GetAllShoppingCartItems();
            ShoppingCart.ShoppingCartItems = items;

            foreach(var item in items)
            {
                if(!ShoppingCart.IsShoppingCartStockValid(item.Album, item.Amount))
                {
                    ModelState.AddModelError(item.Album.AlbumId.ToString(), String.Format("Sorry! There are currently only {0} of this item in stock", item.Album.InStock));
                }
            }

            if(ShoppingCart.ShoppingCartItems.Count == 0)
            {
                ViewBag.Message = "Your shopping cart is currently empty";
            }
            else
            {
                ViewBag.Message = "Here are the albums in your shopping cart";
            }

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = ShoppingCart,
                Total = ShoppingCart.GetCartTotal()
            };

            return View(sCVM);
        }

        public ActionResult AddAlbumToCart(int albumId, int albumAmount=0)
        {
            var selectedAlbum = _albumRepository.Albums.FirstOrDefault(d => d.AlbumId == albumId);
            if (selectedAlbum != null)
            {
                if(albumAmount > 0)
                {
                    if (ShoppingCart.EnoughInStock(selectedAlbum, albumAmount))
                    {
                        ShoppingCart.AddToCart(selectedAlbum, albumAmount);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "There is not enough of that item in stock!";
                        return View("AddToCartError", selectedAlbum);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "There was an error processing your request!";
                    return View("AddToCartError", selectedAlbum);
                }
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int albumId)
        {
            var selectedAlbum = _albumRepository.Albums.FirstOrDefault(d => d.AlbumId == albumId);
            if (selectedAlbum != null)
            {
                ShoppingCart.RemoveFromCart(selectedAlbum);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            ShoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}