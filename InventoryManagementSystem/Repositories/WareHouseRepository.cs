using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories
{
    public class WareHouseRepository:IWareHouseRepo
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
           db.WareHouses.Remove(GetById(id));
        }

        public void Update(WareHouse obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public List<WareHouse> GetAll()
        {
            return db.WareHouses.ToList();
        }

        public WareHouse GetById(int? id)
        {
            return db.WareHouses.Find(id);
        }

        public List<WareHouse> sort(string sortOrder)
        {

            var wareHouses = from w in db.WareHouses
                        select w;

            wareHouses = sortOrder switch
            {
                "id_desc" => wareHouses.OrderByDescending(u => u.WareHouseId),
                "name_desc" => wareHouses.OrderByDescending(u => u.Name),
                "loca" => wareHouses.OrderBy(u => u.Location),
                "loca_desc" => wareHouses.OrderByDescending(u => u.Location),
                "phone" => wareHouses.OrderBy(u => u.PhoneNumber),
                "phone_desc" => wareHouses.OrderByDescending(u => u.PhoneNumber),
                _ => wareHouses.OrderBy(u => u.WareHouseId),
            };
            return wareHouses.ToList();
        }
    }
}
