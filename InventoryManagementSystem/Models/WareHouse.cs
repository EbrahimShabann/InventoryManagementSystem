using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("WareHouse")]
    public class WareHouse
    {
        public int WareHouseId { get; set; }


        [Required]
        [Remote("CheckUniqueness", "AttributesConstraints")]
        [MaxLength(100,ErrorMessage ="Name length can't be more than 100 chars")]
        public string Name { get; set; }


        [Required]
        [MaxLength(255, ErrorMessage = "Location length can't be more than 255 chars")]
        public string Location { get; set; }


        [DataType(DataType.PhoneNumber)]
        [MaxLength(100, ErrorMessage = "Phone Number length can't be more than 20  number")]
        public string PhoneNumber { get; set; }


        [Required]
        public bool IsActive { get; set; }

        public virtual List<InventoryItem> InventoryItems { get; set; }

    }
}
