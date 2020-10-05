using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Inventory : Entity
    {
        public virtual List<InventoryItem> InventoryItems { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}