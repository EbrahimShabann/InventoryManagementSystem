using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly AppDbContext db;
        List<Transaction> transactions;
        public TransactionRepository(AppDbContext db)
        {
            this.db = db;
            transactions = this.db.Transactions.OrderByDescending(t => t.TransactionDate).ToList();

        }

        public void Add(Transaction obj)
        {
            db.Transactions.Add(obj);
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetAll()
        {
            List<Transaction> transactionViewModels = new List<Transaction>();
            foreach (var transaction in transactions) transactionViewModels.Add(FillTransactionViewModel(transaction));
            
            return transactionViewModels;
        }
            Transaction FillTransactionViewModel(Transaction transaction)
        {
            string type = "";
            switch (transaction.TransactionType.ToLower())
            {
                case "in":
                    type = "incoming";
                    break;
                case "out":
                    type = "outgoing";
                    break;
                case "adj":
                    type = "adjustment";
                    break;
            }
            return transaction;
        }

        public Transaction GetById(int? id)
        {
            return transactions.FirstOrDefault(t => t.TransactionId == id);
        }

        public JsonResult GetCurrentStock(int productId, int warehouseId)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsByDate(DateTime date)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in this.transactions)
            {
                if(transaction.TransactionDate == date) transactions.Add(transaction);
            }
            return null;
        }

        public void Update(Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}
