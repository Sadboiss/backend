using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Controllers.DTOs;

namespace WebApi.Entities
{
    public class ShoppingCart : Entity
    {
        public Guid UserId { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public ShoppingCart()
        {
            CartItems = new List<CartItem>();
        }
    }
}