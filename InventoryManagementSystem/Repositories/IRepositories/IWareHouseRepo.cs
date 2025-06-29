
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IWareHouseRepo:IRepository<WareHouse>
    {
        List<WareHouse> sort(string sortparam);
    }
}
