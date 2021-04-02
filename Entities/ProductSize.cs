using System;

namespace WebApi.Entities
{
    public class ProductSize : Entity
    {
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public int InStock { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Size  Size { get; set; }
    }
}