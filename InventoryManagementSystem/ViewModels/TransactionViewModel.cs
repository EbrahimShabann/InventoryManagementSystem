using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class TransactionViewModel
    {


            [Required]
            [Display(Name = "Category")]
            public int CategoryId { get; set; }

            [Required]
            [Display(Name = "Product")]
            public int ProductId { get; set; }

            [Required]
            [Display(Name = "Transaction Type")]
            public TransactionType TransactionType { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive")]
            public int QuantityChange { get; set; }

            [Required]
            [Display(Name = "Warehouse")]
            public int WarehouseId { get; set; }
        
    }
}
