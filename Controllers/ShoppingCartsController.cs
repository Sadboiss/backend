using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ShoppingCartDto> GetCart(int id)
        {
            var shoppingCart = _shoppingCartService.GetCart(id).First();
            var mappedEntity = _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
            return Ok(mappedEntity);
        }

        [HttpPost("{userId}/clear")]
        public IActionResult Clear(int userId)
        {
            return Ok(_shoppingCartService.Clear(userId));
        }
        
        [HttpPost("add")]
        public IActionResult Add([FromBody] CartItemDto model)
        {
            return Ok(_shoppingCartService.Add(model));
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_shoppingCartService.Delete(id));
        }
    }
}