using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public sealed class CartItemResponse
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        public CartItemResponse(CartItem cI)
        {
            ProductId = cI.ProductId;
            CartId = cI.ShoppingCartId;
            Quantity = cI.Quantity;
            DateCreated = cI.DateCreated;
        }
    }
}