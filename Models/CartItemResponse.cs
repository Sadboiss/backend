using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public sealed class CartItemResponse
    {
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        public CartItemResponse(CartItem cI)
        {
            ItemId = cI.ItemId;
            CartId = cI.CartId;
            Quantity = cI.Quantity;
            DateCreated = cI.DateCreated;
        }
    }
}