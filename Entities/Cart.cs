using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Cart : Entity
    {
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        
        public DateTime DateCreated;
        public virtual User User { get; set; }
    }
}