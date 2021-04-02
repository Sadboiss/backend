using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class CartItemDto
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProductId { get; set; }
        public string Quantity { get; set; }
    }
}