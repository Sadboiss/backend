using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Controllers.DTOs;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private SadboisContext _context;
        private readonly IMapper _mapper;
        
        public ProductsController(SadboisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Products
                .ToList()
                .Select(product => _mapper.Map<Product, ProductDto>(product))
                .ToList()
            );
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_context.Products
                .Where(product => product.Id == id)
                .Select(product => _mapper.Map<Product, ProductDto>(product))
                .First()
            );
        }
        
        [AllowAnonymous]
        [HttpPost("add")]
        public IActionResult Add([FromBody] ProductDto model)
        {
            if (model == null) return null;
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Image = model.Image
            };
            _context.Products.Add(product);
            return Ok(_context.SaveChanges() > 0);
        }
        
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) 
                return NotFound(new {message = "Cannot find the product you are trying to delete"});
            _context.Products.Remove(product);
            return Ok(_context.SaveChanges() > 0);

        }
    }
}