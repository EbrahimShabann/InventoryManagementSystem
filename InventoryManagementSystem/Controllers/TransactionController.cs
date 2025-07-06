using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        ITransactionRepository transaction;
        public TransactionController(ITransactionRepository transaction)
        {
            this.transaction = transaction;
        }

        public IActionResult GetAllTransactions(int pageNumber = 1, int pageSize = 10)
        {
            int In =0, Out =0, adj = 0;
            foreach (var item in transaction.GetAll())
            {
                if (item.TransactionType.ToLower() == "in")
                {
                    In++;
                }
                else if (item.TransactionType.ToLower() == "out")
                {
                    Out++;
                }
                else if (item.TransactionType.ToLower() == "adj")
                {
                    adj++;
                }
            }
            ViewBag.countout = Out;
            ViewBag.countin = In;
            ViewBag.countadj = adj;
            var  transactions = transaction.GetAll().AsEnumerable();
            PagedResult<Models.Transaction> pagedResult = transactions.ToPagedResult(pageNumber, pageSize);
            return View(pagedResult);

          
        }

        public IActionResult GetTransactionById(int id)
        {
            Transaction tr = transaction.GetById(id);
            switch (tr.TransactionType.ToLower())
            {
                case "in":
                    ViewBag.TransactionType = "incoming";
                    ViewBag.QuantityStyle = "quantity-positive";
                    break;
                case "out":
                    ViewBag.TransactionType = "outgoing";
                    ViewBag.QuantityStyle = "quantity-negative";
                    break;
                case "adj":
                    ViewBag.TransactionType = "adjustment";
                    ViewBag.QuantityStyle = "quantity-neutral";
                    break;
            }
            if (tr.WareHouse.IsActive)
            {
                ViewBag.WareHouseStyle = "bg-success";
                ViewBag.WareHouseStatus = "Active";
            }
            else { 
                ViewBag.WareHouseStyle = "bg-danger";
                ViewBag.WareHouseStatus = "Inactive";

            }
            ViewBag.UserTransactions = tr.User.Transactions.OrderByDescending(t => t.TransactionDate).ToList();
            return View(transaction.GetById(id));
        }
        public IActionResult DeleteTransaction(int id)
        {
            return View();
        }
        public IActionResult AddTransaction()
        {
            return View();
        }
        public IActionResult UpdateTransaction(int id)
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
