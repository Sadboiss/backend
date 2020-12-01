using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Middleware;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly SadboisContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartsController(SadboisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetCart(int id)
        {
            Console.WriteLine("GetCart");
            return await _context.ShoppingCarts
                .Where(shoppingCart => shoppingCart.Id == id)
                .Include(shoppingCart => shoppingCart.CartItems)
                .ThenInclude(product => product.Product)
                .Select(shoppingCart => _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart))
                .FirstAsync();
        }

        [HttpPost("{userId}/clear")]
        public async Task<IActionResult> Clear(int userId)
        {
            _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Include(x => x.CartItems)
                .First().CartItems
                .Clear();
            return Ok(await _context.SaveChangesAsync());
        }
        
        [HttpPost("{cartId}/add/{productId}")]
        public async Task<IActionResult> Add(int cartId, int productId)
        {
            var cartItems = _context.CartItems.Where(x => x.ShoppingCartId == cartId);
            if (cartItems.Any(x => x.ProductId == productId))
            {
                cartItems.First(x => x.ProductId == productId).Quantity++;
            }
            else
            {
                var item = new CartItem
                {
                    ShoppingCartId = cartId,
                    ProductId = productId,
                    Quantity = 1,
                };
                _context.CartItems.Add(item);
            }
            return Ok(await _context.SaveChangesAsync() > 0);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
            if (cartItem == null) return Ok(-1);
            _context.CartItems.Remove(cartItem);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}