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
        IEnumerable<ShoppingCart> GetCart(int id);
        int Add(CartItemDto model);
        int Delete(int id);
        int Clear(int userId);
    }
    public class ShoppingCartService : IShoppingCartService
    {
        private SadboisContext _context;
        
        public ShoppingCartService(SadboisContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingCart> GetCart(int id)
        {
            return _context.ShoppingCarts
                .Where(s => s.Id == id)
                .Include(s => s.CartItems)
                .ThenInclude(x => x.Product);
        }

        public int Add(CartItemDto model)
        {
            var item = new CartItem
            {
                
                ShoppingCartId = model.ShoppingCartId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
            _context.CartItems.Add(item);
            return _context.SaveChanges();
        }

        public int Clear(int userId)
        {
            _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(x => x.CartItems)
                .First().CartItems
                .Clear();
            return _context.SaveChanges();
        }
        public int Delete(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
            if (cartItem == null) return -1;
            _context.CartItems.Remove(cartItem);
            return _context.SaveChanges();
        }
    }
}