using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IWareHouseRepo warehouseRepo { get; }
        void Save();

    }
}
