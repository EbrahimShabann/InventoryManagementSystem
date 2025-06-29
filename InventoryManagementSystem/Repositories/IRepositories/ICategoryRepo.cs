using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface ICategoryRepo : IRepository<Category>
    {
        List<Category> Sort(string sortParam);
    }
}
