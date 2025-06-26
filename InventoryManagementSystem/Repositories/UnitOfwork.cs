using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext db;
        IWareHouseRepo _wareHouseRepo;
        IAppUserRepo userRepo;
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
        public IAppUserRepo AppUserRepo { get
            {
                if (userRepo != null) return userRepo;
                userRepo = new AppUserRepo(db);
                return userRepo ;
            } }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
