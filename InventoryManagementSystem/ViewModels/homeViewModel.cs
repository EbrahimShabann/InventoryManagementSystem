using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;

namespace InventoryManagementSystem.ViewModels
{
    public class homeViewModel
    {
      
        public int warehousesNum { get; set; }
        public int categoriesNum { get; set; }
        public int productsNum { get; set; }
        public int suppliersNum { get; set; }
        public IEnumerable<Product> LowStockProducts { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
