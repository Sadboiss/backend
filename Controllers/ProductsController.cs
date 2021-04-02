using System;
using System.Collections.Generic;
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
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SadboisContext _context;
        private readonly IMapper _mapper;
        private readonly IProductImagesService _service;
        public ProductsController(SadboisContext context, IMapper mapper, IProductImagesService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var x = await _context.Products
                .Include(product => product.Category)
                .Include(product => product.ProductSizes)
                .Include(product => product.ProductImages)
                .Select(product => _mapper.Map<Product, ProductDto>(product))
                .ToListAsync();
            return Ok(x);
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(_context.Products
                .Where(product => product.Id.ToString().Equals(id))
                .Select(product => _mapper.Map<Product, ProductDto>(product))
                .First()
            );
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromForm] ProductDto model)
        {
            if (model == null) return null;
            var productExists = _context.Products.FirstOrDefault(p => p.Id.Equals(model.Id));

            if (productExists == null)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId
                };
                var productSizes = _context.Sizes.Select(size =>
                    new ProductSize
                    {
                        Product = product,
                        Size = size,
                        InStock = 0
                    })
                    .ToList();
                _context.Products.Add(product);
                _context.ProductSizes.AddRange(productSizes);
                
                _service.CreateNewImage(model.FileList, product);
            }
            else
            { 
                _context.Entry(productExists).CurrentValues.SetValues(model);
            }
            return Ok(await _context.SaveChangesAsync() > 0);
        }
        
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id.ToString().Equals(id));
            if (product == null) 
                return NotFound(new {message = "Cannot find the product you are trying to delete"});
            _context.Products.Remove(product);
            return Ok(await _context.SaveChangesAsync() > 0);

        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(product.Id));
            if (existingProduct == null)
                return BadRequest(new { message = "The product you are trying to update does not exist." });
            _context.Entry(existingProduct).CurrentValues.SetValues(product);  
            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }
}