using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Transactions;

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
            List<Models.Transaction> transactionViewModels = new List<Models.Transaction>();
            foreach (var transaction in transactions) transactionViewModels.Add(FillTransactionViewModel(transaction));
            
            return transactionViewModels;
        }
        Models.Transaction FillTransactionViewModel(Models.Transaction transaction)
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

        public Models.Transaction GetById(int? id)
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

        public List<Models.Transaction> GetTransactionsByDate(DateTime date)
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();
            foreach (var transaction in this.transactions)
            {
                if(transaction.TransactionDate == date) transactions.Add(transaction);
            }
            return null;
        }

        public void Update(Models.Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}
