using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApi.Entities
{
    public class Order : Entity
    {
        
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}