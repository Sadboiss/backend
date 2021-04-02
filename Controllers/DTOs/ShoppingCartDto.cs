using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class ShoppingCartDto : Entity
    {
        public Guid UserId { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public virtual User User { get; set; }
        public int ItemsCount => CartItems.Count;
        public double TotalPrice => CartItems.Select(x => x.Product.Price * x.Quantity).Sum();
    }
}