using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class GetAllTransactionsViewModel
    {

            public int TransactionId { get; set; }

            [Required]
            [Display(Name = "Category")]
            public string CategoryName { get; set; }

            [Required]
            [Display(Name = "Product")]
            public string ProductName { get; set; }

            [Required]
            [Display(Name = "Transaction Type")]
            public String TransactionType { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive")]
            public int QuantityChange { get; set; }
            [Required]
            public DateTime TransactionDate { get; set; }
        [Required]
            [Display(Name = "Warehouse")]
            public string WarehouseName { get; set; }
        
    }
}
