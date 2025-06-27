using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface ISupplierRepo: IRepository<Supplier>
    {
        List<Supplier> sort(string sortparam);
    }

}
