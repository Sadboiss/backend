using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class ProductDto : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
    }
}