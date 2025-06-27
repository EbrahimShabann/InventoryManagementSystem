using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace InventoryManagementSystem.Controllers
{
    public class TransactionController : Controller
    {
        ITransactionRepository transaction;
        public TransactionController(ITransactionRepository transaction)
        {
            this.transaction = transaction;
        }

        public IActionResult GetAllTransactions()
        {
            return View();
        }
        public IActionResult GetTransactionsByDate(DateTime date)
        {
            return View();
        }
        public IActionResult GetTransactionsById(int id)
        {
            return View();
        }
        public JsonResult GetProductsByCategory(int categoryId)
        {
            //var products = db.Products
            //    .Where(p => p.CategoryId == categoryId)
            //    .Select(p => new {
            //        Id = p.Id,
            //        Name = p.Name
            //    }).ToList();

            //return Json(products, JsonRequestBehavior.AllowGet);
            return null;
        }

        public JsonResult GetCurrentStock(int productId, int warehouseId)
        {
            //var stock = db.Inventories
            //    .Where(i => i.ProductId == productId
            //             && i.WarehouseId == warehouseId)
            //    .Select(i => i.Quantity)
            //    .FirstOrDefault();

            //return Json(stock, JsonRequestBehavior.AllowGet);
            return null;
        }
    }

}
