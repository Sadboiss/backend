using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public class CartItemRequest
    {
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Item Item { get; set; }
    }
}