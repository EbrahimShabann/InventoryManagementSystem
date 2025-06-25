using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services.ViewModels
{
    public class WareHouseVM
    {
        public List<WareHouse> WareHouses { get; set; }
        public string SortParam { get; set; }
    }
}
