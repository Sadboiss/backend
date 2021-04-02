using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CartItem : Entity
    {
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}