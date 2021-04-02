using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingCart> GetCart(string id);
        int Add(string userId, string productId);
        int Delete(string id);
        int Clear(string userId);
    }
    public class ShoppingCartService : IShoppingCartService
    {
        private SadboisContext _context;
        
        public ShoppingCartService(SadboisContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingCart> GetCart(string id)
        {
            return _context.ShoppingCarts
                .Where(s => s.Id.ToString().Equals(id))
                .Include(s => s.CartItems)
                .ThenInclude(x => x.Product);
        }

        public int Add(string cartId, string productId)
        {
            var cartItems = _context.CartItems.Where(x => x.ShoppingCartId.ToString().Equals(cartId));
            if (cartItems.Any(x => x.ProductId.ToString().Equals(productId)))
            {
                cartItems.First(x => x.ProductId.ToString().Equals(productId)).Quantity++;
            }
            else
            {
                var item = new CartItem
                {
                    ShoppingCartId = Guid.Parse(cartId),
                    ProductId = Guid.Parse(productId),
                    Quantity = 1,
                };
                _context.CartItems.Add(item);
            }
            return _context.SaveChanges();
        }

        public int Clear(string userId)
        {
            _context.ShoppingCarts
                .Where(c => c.UserId.ToString().Equals(userId))
                .Include(x => x.CartItems)
                .First().CartItems
                .Clear();
            return _context.SaveChanges();
        }
        public int Delete(string id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id.ToString().Equals(id));
            if (cartItem == null) return -1;
            _context.CartItems.Remove(cartItem);
            return _context.SaveChanges();
        }
    }
}