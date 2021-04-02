using System;

namespace WebApi.Controllers.DTOs
{
    public class ProductImageDto
    {
        public Guid ProductId { get; set; }
        public byte[] ImageData { get; set; }
    }
}