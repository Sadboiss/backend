using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine("GetAll");
            return Ok(await _context.Products
                .Include(product => product.Category)
                .Select(product => _mapper.Map<Product, ProductDto>(product))
                .ToListAsync()
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
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] ProductDto model)
        {
            if (model == null) return null;
            var productExists = _context.Products.FirstOrDefault(x => x.Id == model.Id);
            
            if (model.File != null && model.File.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await model.File.CopyToAsync(stream);
                    model.Image = stream.ToArray();
                }
            }

            if (productExists == null)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Image = model.Image,
                    CategoryId = model.CategoryId
                };
                _context.Products.Add(product);
            }
            else
            { 
                _context.Entry(productExists).CurrentValues.SetValues(model);
            }
            return Ok(await _context.SaveChangesAsync() > 0);
        }
        
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) 
                return NotFound(new {message = "Cannot find the product you are trying to delete"});
            _context.Products.Remove(product);
            return Ok(await _context.SaveChangesAsync() > 0);

        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null)
                return BadRequest(new { message = "The product you are trying to update does not exist." });
            _context.Entry(existingProduct).CurrentValues.SetValues(product);  
            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }
}