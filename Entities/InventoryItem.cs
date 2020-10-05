using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class InventoryItem : Entity
    {
        [ForeignKey("InventoryId")]
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
        
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}