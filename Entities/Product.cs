using System;

namespace WebApi.Entities
{
    public class Product : Entity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        
        public Category Category { get; set; }
    }
}