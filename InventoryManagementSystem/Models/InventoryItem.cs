using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        [Required]
        public int WareHouseId { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual WareHouse WareHouse { get; set; }

        [Required]
        [Range(0,double.MaxValue,ErrorMessage ="Quantity can't be less than 0")]
        public int Quantity { get; set; }


        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
