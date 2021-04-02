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
        
        [HttpGet("{userId}")]
        public async Task<ActionResult<ShoppingCartDto>> GetCart(Guid userId)
        {
            Console.WriteLine(userId);
            return await _context.ShoppingCarts
                .Where(shoppingCart => shoppingCart.UserId.Equals(userId))
                .Include(shoppingCart => shoppingCart.CartItems)
                .ThenInclude(product => product.Product)
                .Select(shoppingCart => _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart))
                .FirstAsync();
        }
        [HttpGet("{userId}/count")]
        public IActionResult GetItemsCount(Guid userId)
        {
            var cartItems = _context.ShoppingCarts
                .Where(x => x.UserId.Equals(userId))
                .Include(x => x.CartItems)
                .Select(x => x.CartItems)
                .FirstOrDefault();
            if (cartItems == null)
                return BadRequest(new {message = "Problem while counting your items"});
            var totalCount = cartItems.Select(x => x.Quantity).Sum();
            return Ok(totalCount);
        }

        [HttpPost("{userId}/clear")]
        public async Task<IActionResult> Clear(Guid userId)
        {
            _context.ShoppingCarts
                .Where(c => c.UserId.Equals(userId))
                .Include(x => x.CartItems)
                .First().CartItems
                .Clear();
            return Ok(await _context.SaveChangesAsync());
        }
        
        [HttpPost("{cartId}/add/{productId}")]
        public async Task<IActionResult> Add(Guid cartId, Guid productId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.ShoppingCartId.Equals(cartId));
            if (cartItems.Any(ci => ci.ProductId.Equals(productId)))
            {
                cartItems.First(ci => ci.ProductId.Equals(productId)).Quantity++;
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
        public async Task<IActionResult> Delete(string id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id.ToString().Equals(id));
            if (cartItem == null) return Ok(-1);
            _context.CartItems.Remove(cartItem);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}