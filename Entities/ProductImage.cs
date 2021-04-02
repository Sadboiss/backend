using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Entities
{
    public class ProductImage : Entity
    {
        public Guid ProductId { get; set; }
        public byte[] ImageData { get; set; }
        public virtual Product Product { get; set; }
    }
}