using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.Build.Evaluation;

namespace InventoryManagementSystem.Repositories
{
    public class ProdcutRepo : IproductRepo
    {
        AppDbContext context;
        public ProdcutRepo(AppDbContext _context)

        {
            context = _context;

        }

        public void Add(Product obj)
        {
            context.Products.Add(obj);
        }

        public void Delete(int? id)
        {
          Product obj = GetById(id);
          context.Products.Remove(obj);
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int? id)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public void Update(Product obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void save()
        {
            context.SaveChanges();
        }
    }
}
