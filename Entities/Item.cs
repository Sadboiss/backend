using System;

namespace WebApi.Entities
{
    public class Item : Entity
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelYear { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
    }
}