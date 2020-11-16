using System;

namespace WebApi.Entities
{
    public class Item : Entity
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
    }
}