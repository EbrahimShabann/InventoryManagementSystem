using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext db;
        IWareHouseRepo _wareHouseRepo;
        public UnitOfwork(AppDbContext db)
        {
            this.db = db;
        }
        public IWareHouseRepo warehouseRepo { get
            {
                if (_wareHouseRepo != null) return _wareHouseRepo;
                _wareHouseRepo = new WareHouseRepository(db);
                return _wareHouseRepo ;
            } }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
