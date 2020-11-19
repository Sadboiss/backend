using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class ShoppingCartDto : Entity
    {
        public int UserId { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public virtual User User { get; set; }
    }
}