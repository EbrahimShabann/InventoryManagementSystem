using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IInventoryItemRepo : IRepository<InventoryItem>
    {
        List<InventoryItem> sort(string sortparam);
    }
}
