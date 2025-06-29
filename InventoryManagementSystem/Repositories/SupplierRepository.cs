using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class SupplierRepository:ISupplierRepo
    {
        private readonly AppDbContext db;

        public SupplierRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<Supplier> GetAll()
        {
            return db.Suppliers.ToList();
        }

        public Supplier GetById(int? id)
        {
            return db.Suppliers.Find(id);
        }

        public void Add(Supplier entity)
        {
            db.Suppliers.Add(entity);
        }

        public void Update(Supplier obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int? id)
        {
            db.Suppliers.Remove(GetById(id));
        }

        public List<Supplier> sort(string sortparam)
        {

            var suppliers = from s in db.Suppliers
                            select s;

            suppliers = sortparam switch
            {
                "id_desc" => suppliers.OrderByDescending(u => u.SupplierId),
                "name" => suppliers.OrderBy(u => u.Name),
                "name_desc" => suppliers.OrderByDescending(u => u.Name),
                "email" => suppliers.OrderBy(u => u.Email),
                "email_desc" => suppliers.OrderByDescending(u => u.Email),

                _ => suppliers.OrderBy(u => u.SupplierId),
            };
            return suppliers.ToList();
        }
    }
}
