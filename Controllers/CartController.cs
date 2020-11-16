using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        [HttpGet("{userId}/items")]
        public IActionResult GetItems(int id)
        {
            var cart = _cartService.GetById(id);
            if (cart == null)
                return NotFound(new { message = "Cannot find cart belonging to that user." });
            return Ok(cart);
        }
        
        [HttpGet("{userId}/item/{itemId}")]
        public IActionResult GetItem(int userId, int itemId)
        {
            var cart = _cartService.GetById(id);
            if (cart == null)
                return NotFound(new { message = "Cannot find inventory belonging to that user." });
            return Ok(cart);
        }
        
        [HttpPost("add-item")]
        public IActionResult AddToCart([FromBody] CartItemRequest model)
        {
            var cartItem = _cartService.AddItemToCart(model);
            if(cartItem == null)
                return BadRequest(new { message = "Error while adding item to cart." });
            return Ok(cartItem);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var cart = _cartService.GetById(id);
            if (cart == null)
                return NotFound(new { message = "Cannot find cart belonging to that user." });
            return Ok(cart);
        }
    }
}