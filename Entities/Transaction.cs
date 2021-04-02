using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Transaction : Entity
    {
        public double Price { get; set; }    
        public bool Successful { get; set; }
        
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        
        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}