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

        private readonly AppDbContext DbContext;

        public ShoppingCart(AppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetRequiredService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Album album, int amount)
        {
            var shoppingCartItem = DbContext.CartItems.SingleOrDefault(s =>
            s.Album.AlbumId == album.AlbumId && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                        Album = album,
                        ShoppingCartId = this.ShoppingCartId,
                        Amount = amount
                    };

                DbContext.CartItems.Add(shoppingCartItem);
            }      

            else
            {
                shoppingCartItem.Amount += amount;
            }
            DbContext.SaveChanges();
        }

        public bool EnoughInStock(Album album, int amount)
        {
            var shoppingCartItem = DbContext.CartItems.SingleOrDefault(s =>
            s.Album.AlbumId == album.AlbumId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                return (amount > album.InStock ? false : true);
            }
            else
            {
                return (shoppingCartItem.Amount + amount > shoppingCartItem.Album.InStock ? true : false);
            }
        }

        public int RemoveFromCart(Album album)
        {
            var shoppingCartItem = DbContext.CartItems.SingleOrDefault(s =>
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
                    DbContext.CartItems.Remove(shoppingCartItem);
                }
            }

            DbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = DbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                   .Include(s => s.Album)
                   .ToList());
        }

        public void ClearCart()
        {
            var items = DbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

            DbContext.CartItems.RemoveRange(items);
            DbContext.SaveChanges();
        }

        public decimal GetCartTotal()
        {
            return DbContext.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                                                  .Select(c => c.Album.Price * c.Amount).Sum();
        }
    }
}