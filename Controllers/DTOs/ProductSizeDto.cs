using System.Drawing;

namespace WebApi.Controllers.DTOs
{
    public class ProductSizeDto
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int InStock { get; set; }
    }
}