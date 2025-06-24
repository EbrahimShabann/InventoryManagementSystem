using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext db;
        public IWareHouseRepository WareHouse { get; }

        public UnitOfWork(AppDbContext db)
        {
            this.db = db;
            WareHouse=new WareHouseRepository(db);
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
