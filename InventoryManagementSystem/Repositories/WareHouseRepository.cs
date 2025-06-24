using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly AppDbContext db;

        public WareHouseRepository(AppDbContext db)
        {
            this.db = db;
        }
        public void Add(WareHouse obj)
        {
            db.WareHouses.Add(obj);
            
        }

        public void Delete(int? id)
        {
            var wareHouseFromDb = db.WareHouses.SingleOrDefault(w=>w.WareHouseId==id);
            if (wareHouseFromDb != null) 
            {
                db.WareHouses.Remove(wareHouseFromDb);
            }
        }

        public List<WareHouse> GetAll()
        {
            return db.WareHouses.ToList();
        }

        public WareHouse GetById(int? id)
        {
                return db.WareHouses.Find(id);
        
        }

        

        public void Update(WareHouse obj)
        {
           if(obj!= null)
            {
                db.WareHouses.Update(obj);
            }
        }
    }
}
