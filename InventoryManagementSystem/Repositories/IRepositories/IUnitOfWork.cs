using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IWareHouseRepo warehouseRepo { get; }
        public IAppUserRepo AppUserRepo { get;  }
        void Save();

    }
}
