using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;

namespace InventoryManagementSystem.Repositories
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly AppDbContext db;
        public CategoryRepository(AppDbContext db)
        {
            this.db = db;
        }
        public void Add(Category obj) => db.Categories.Add(obj);
        public void Delete(int? id)
        {
            var cat = db.Categories.Find(id);
            if (cat != null) db.Categories.Remove(cat);
        }
        public List<Category> GetAll() => db.Categories.ToList();
        public Category GetById(int? id) => db.Categories.FirstOrDefault(c => c.CategoryId == id);
        public void Update(Category obj) => db.Categories.Update(obj);
        public List<Category> Sort(string sortParam)
        {
            return sortParam switch
            {
                "name" => db.Categories.OrderBy(c => c.Name).ToList(),
                "name_desc" => db.Categories.OrderByDescending(c => c.Name).ToList(),
                _ => db.Categories.OrderBy(c => c.CategoryId).ToList(),
            };
        }
    }
}