using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class InventoryItemRepository : IInventoryItemRepo
    {
        private readonly AppDbContext db;

        public InventoryItemRepository(AppDbContext db)
        {
            this.db = db;
        }

        public void Add(InventoryItem obj)
        {
            db.InventoryItems.Add(obj);
        }

        public void Delete(int? id)
        {
            db.InventoryItems.Remove(GetById(id));
        }

        public void Update(InventoryItem obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public List<InventoryItem> GetAll()
        {
            return db.InventoryItems.ToList();
        }

        public InventoryItem GetById(int? id)
        {
            return db.InventoryItems.Find(id);
        }

        public List<InventoryItem> sort(string sortOrder)
        {
            var inventoryItems = from i in db.InventoryItems
                                 select i;

            inventoryItems = sortOrder switch
            {
                "id" => inventoryItems.OrderBy(u => u.InventoryItemId),
                "id_desc" => inventoryItems.OrderByDescending(u => u.InventoryItemId),
                "warehouse" => inventoryItems.OrderBy(u => u.WareHouseId),
                "warehouse_desc" => inventoryItems.OrderByDescending(u => u.WareHouseId),
                "quantity" => inventoryItems.OrderBy(u => u.Quantity),
                "quantity_desc" => inventoryItems.OrderByDescending(u => u.Quantity),
                _ => inventoryItems.OrderBy(u => u.InventoryItemId),
            };
            return inventoryItems.ToList();
        }
    }
}
