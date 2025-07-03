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
        [Remote("CheckWHNameUniq", "AttributesConstraints",AdditionalFields = "WareHouseId")]
        [MaxLength(100,ErrorMessage ="Name length can't be more than 100 chars")]
        public string Name { get; set; }


        [Required]
        [MinLength(20,ErrorMessage = "Location length can't be less than 20 chars")]
        [MaxLength(255, ErrorMessage = "Location length can't be more than 255 chars")]
        public string Location { get; set; }


        [RegularExpression("^(?:\\+20|0020|0)?1[0125]\\d{8}$"
            ,ErrorMessage = "Phone Number Must be one of these formats +20xxxxxxxxxx or  0020xxxxxxxxxx or 0xxxxxxxxxx")]
        public string PhoneNumber { get; set; }


        [Required]
        public bool IsActive { get; set; }

        public virtual List<InventoryItem> InventoryItems { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser Manager { get; set; }

    }
}
