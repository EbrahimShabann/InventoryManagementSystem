using System.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Repositories.IRepositories
{
    public interface ITransactionRepository:IRepository<Models.Transaction>
    {
        public List<Models.Transaction> GetTransactionsByDate(DateTime date);
        public JsonResult GetProductsByCategory(int categoryId);
        public JsonResult GetCurrentStock(int productId, int warehouseId);


    }
}
