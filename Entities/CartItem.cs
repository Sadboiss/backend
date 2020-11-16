using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CartItem : Entity
    {
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        [ForeignKey("CartId")]
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Item Item { get; set; }
        public virtual Cart Cart { get; set; }
   
    }
}