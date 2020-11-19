using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Transaction : Entity
    {
        public double Price { get; set; }    
        public bool Successful { get; set; }
        
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}