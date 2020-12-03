using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public bool Display { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public CategoryDto Category { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
    }
}