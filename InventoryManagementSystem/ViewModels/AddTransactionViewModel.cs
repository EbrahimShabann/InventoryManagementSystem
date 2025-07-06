using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSystem.Models;
namespace InventoryManagementSystem.ViewModels
{
    public class AddTransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> Products { get; set; }
        public List<string> TransactionTypes { get; set; } = new List<string> { "In", "Out", "Adj" };
        public int CategoryId { get; set; }

        // For adjustment transactions
        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
    }
}