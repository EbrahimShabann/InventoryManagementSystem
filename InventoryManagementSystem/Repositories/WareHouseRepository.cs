using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventoryManagementSystem.Repositories
{
    public class WareHouseRepository:IWareHouseRepo
    {
        private readonly AppDbContext db;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public WareHouseRepository(AppDbContext db,UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
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

        public List<WareHouse> sort(string sortOrder, ClaimsPrincipal user)
        {
           IQueryable<WareHouse >wareHouses;
            if (user.IsInRole(StaticDetails.Admin_Role))
                wareHouses = GetAll().AsQueryable();
            else
                wareHouses = GetAll().Where(w=>w.ApplicationUserId==user.FindFirst(ClaimTypes.NameIdentifier).Value).AsQueryable(); 

            wareHouses = sortOrder switch
            {
                "id_desc" => wareHouses.OrderByDescending(u => u.WareHouseId),
                "name_desc" => wareHouses.OrderByDescending(u => u.Name),
                "loca" => wareHouses.OrderBy(u => u.Location),
                "loca_desc" => wareHouses.OrderByDescending(u => u.Location),
                "phone" => wareHouses.OrderBy(u => u.PhoneNumber),
                "phone_desc" => wareHouses.OrderByDescending(u => u.PhoneNumber),
                "products" => wareHouses.OrderBy(u => u.InventoryItems.Count),
                "products_desc" => wareHouses.OrderByDescending(u => u.InventoryItems.Count),
                "date" => wareHouses.OrderBy(u => u.CreatedAt),
                "date_desc" => wareHouses.OrderByDescending(u => u.CreatedAt),
                _ => wareHouses.OrderBy(u => u.WareHouseId),
            };
            return wareHouses.ToList();
        }
    }
}
