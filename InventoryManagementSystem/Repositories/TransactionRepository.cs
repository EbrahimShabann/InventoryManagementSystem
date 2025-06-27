using System.Transactions;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManagementSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext db;
        List<Models.Transaction> transactions;
        public TransactionRepository(AppDbContext db)
        {
            this.db = db;
            transactions = this.db.Transactions.OrderByDescending(t => t.TransactionDate).ToList();

        }

        public void Add(Models.Transaction obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Transaction> GetAll()
        {
            return transactions;
        }

        public Models.Transaction GetById(int? id)
        {
            return transactions.FirstOrDefault(e=> e.TransactionId == id);
        }

        public JsonResult GetCurrentStock(int productId, int warehouseId)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Models.Transaction> GetTransactionsByDate(DateTime date)
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();
            foreach (var transaction in this.transactions)
            {
                if(transaction.TransactionDate == date) transactions.Add(transaction);
            }
            return transactions;
        }

        public void Update(Models.Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}
