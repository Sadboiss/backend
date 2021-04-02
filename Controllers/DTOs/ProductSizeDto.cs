using System;
using System.Drawing;

namespace WebApi.Controllers.DTOs
{
    public class ProductSizeDto
    {
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public string InStock { get; set; }
    }
}