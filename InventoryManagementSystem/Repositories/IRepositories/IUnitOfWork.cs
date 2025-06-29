using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public IWareHouseRepo warehouseRepo { get; }
        public IInventoryItemRepo inventoryItemRepo { get; }
        public ISupplierRepo supplierRepo { get; }
        public IAppUserRepo AppUserRepo { get;  }

        void Save();

    }
}
