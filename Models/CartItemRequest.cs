using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public class CartItemRequest
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Product Product { get; set; }
    }
}