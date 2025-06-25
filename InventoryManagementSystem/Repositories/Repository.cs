using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext db;

        public Repository(AppDbContext db)
        {
            this.db = db;
        }
        public void Add(T obj)
        {
            db.Set<T>().Add(obj);
        }

        public void Delete(int? id)
        {
           db.Set<T>().Remove(GetById(id));
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int? id)
        {
            return db.Set<T>().Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry<T>(obj).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
