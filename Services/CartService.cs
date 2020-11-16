using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        CartItemResponse AddItemToCart(CartItemRequest model);
    }
    public class CartService : ICartService
    {
        private readonly SadboisContext _context;
        
        public CartService(SadboisContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts;
        }
    
        public Cart GetById(int userId)
        {
           return _context.Carts.FirstOrDefault(i => i.UserId == userId);
        }

        public CartItemResponse AddItemToCart(CartItemRequest model)
        {
            if (model == null)
                return null;

            var cartItem = new CartItem
            {
                CartId = model.CartId,
                ItemId = model.ItemId,
                Quantity = model.Quantity,
                DateCreated = DateTime.Now
            };

            var cartItems = _context.Carts.FirstOrDefault(c => c.Id == model.CartId)?.CartItems;
            cartItems?.Add(cartItem);
            _context.SaveChanges();
            
            return new CartItemResponse(cartItem);
        }
    }
}