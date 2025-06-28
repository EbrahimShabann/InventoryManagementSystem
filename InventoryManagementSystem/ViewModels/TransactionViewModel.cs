using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class TransactionViewModel: GetAllTransactionsViewModel
    {
        public int WareHouseId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Location length can't be more than 255 chars")]
        public string Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(100, ErrorMessage = "Phone Number length can't be more than 20  number")]
        public string PhoneNumber { get; set; }
        public int ProductId { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [MaxLength(1000, ErrorMessage = "Description Max Length is 1000 Chars")]
        public string Description { get; set; }


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = " Price can't be less than 0")]
        public double Price { get; set; }



    }
}
