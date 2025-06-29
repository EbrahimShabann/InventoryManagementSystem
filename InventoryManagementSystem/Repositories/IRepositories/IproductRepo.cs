using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IproductRepo:IRepository<Product>
    {
        void save();
    }
}
