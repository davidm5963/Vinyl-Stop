using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylStop.Data.Models
{
    public class ShoppingCart
    {

        private readonly AppDbContext _dbContext;

        public ShoppingCart(AppDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static async Task<ShoppingCart> GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var contextAccessor = services.GetRequiredService<IHttpContextAccessor>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var context = services.GetRequiredService<AppDbContext>();

            var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
            if(user != null)
            {
                if(user.CartId == null)
                {
                    string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
                    session.SetString("CartId", cartId);
                    user.CartId = cartId;
                    return new ShoppingCart(context) { ShoppingCartId = cartId };
                }
                else
                {
                    return new ShoppingCart(context) { ShoppingCartId = user.CartId };
                }
            }
            else
            {
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
                session.SetString("CartId", cartId);
                return new ShoppingCart(context) { ShoppingCartId = cartId };
            }

        }

        public void AddToCart(Album album, int amount)
        {
            var shoppingCartItem =_dbContext.CartItems.SingleOrDefault(s =>
            s.Album.AlbumId == album.AlbumId && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                        Album = album,
                        ShoppingCartId = this.ShoppingCartId,
                        Amount = amount
                    };

               _dbContext.CartItems.Add(shoppingCartItem);
            }      

            else
            {
                shoppingCartItem.Amount += amount;
            }
           _dbContext.SaveChanges();
        }

        public bool EnoughInStock(Album album, int amount)
        {
            var shoppingCartItem =_dbContext.CartItems.SingleOrDefault(s =>
            s.Album.AlbumId == album.AlbumId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                return (amount > album.InStock ? false : true);
            }
            else
            {
                return (shoppingCartItem.Amount + amount > shoppingCartItem.Album.InStock ? false : true);
            }
        }

        public bool IsShoppingCartStockValid(Album album, int amount)
        {
            return (amount > album.InStock ? false : true);
        }


        public int RemoveFromCart(Album album)
        {
            var shoppingCartItem =_dbContext.CartItems.SingleOrDefault(s =>
            s.Album.AlbumId == album.AlbumId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                   _dbContext.CartItems.Remove(shoppingCartItem);
                }
            }

           _dbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =_dbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                   .Include(s => s.Album)
                   .ToList());
        }

        public void ClearCart()
        {
            var items =_dbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

           _dbContext.CartItems.RemoveRange(items);
           _dbContext.SaveChanges();
        }

        public decimal GetCartTotal()
        {
            return _dbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                                                  .Select(c => c.Album.Price * c.Amount).Sum();
        }
    }
}