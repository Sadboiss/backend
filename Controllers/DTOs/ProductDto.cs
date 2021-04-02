using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Display { get; set; }
        [FromForm(Name = "FileList")]
        [JsonIgnore]
        public List<IFormFile> FileList { get; }  = new List<IFormFile>();
        public List<ProductImageDto> ProductImages { get; set; }
        public CategoryDto Category { get; set; }
        public List<ProductSizeDto> ProductSizes { get; set; }
    }
}