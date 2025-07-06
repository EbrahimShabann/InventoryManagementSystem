using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {

        private ITransactionRepository transactionRepo;
        private ICategoryRepo categoryRepo;
        private IWareHouseRepo warehouseRepo;
        private IproductRepo productRepo;
        private IInventoryItemRepo inventoryRepo;
        public TransactionController(
        ITransactionRepository transactionRepo,
        ICategoryRepo categoryRepo,
        IWareHouseRepo warehouseRepo,
        IproductRepo productRepo,
        IInventoryItemRepo inventoryRepo)
        {
            this.transactionRepo = transactionRepo;
            this.categoryRepo = categoryRepo;
            this.warehouseRepo = warehouseRepo;
            this.productRepo = productRepo;
            this.inventoryRepo = inventoryRepo;
        }
        public bool IsDateInRange(DateTime inputDate, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null && endDate == null)
                return true;

            if (endDate == null)
                return inputDate >= startDate.Value;

            if (startDate == null)
            {
                return inputDate <= endDate.Value.AddDays(1);
            }

            return inputDate >= startDate.Value && inputDate <= endDate.Value;
        }
        public IActionResult GetAllTransactions(int WareHouseId, DateTime? startDate, DateTime? endDate, int page = 1, int size = 10)
        {
            // Get filtered transactions (fixed to use filtered list for counts)
            var allTransactions = transactionRepo.GetAll();

            // Apply date filter
            var filteredTransactions = allTransactions
                .Where(t => IsDateInRange(t.TransactionDate, startDate, endDate))
                .ToList();

            // Apply warehouse filter if specified
            if (WareHouseId != 0)
            {
                filteredTransactions = filteredTransactions
                    .Where(t => t.WareHouseId == WareHouseId)
                    .ToList();
            }

            // Count transaction types FROM FILTERED SET
            int In = 0, Out = 0, adj = 0;
            foreach (var item in filteredTransactions)
            {
                if (item.TransactionType.Equals("in", StringComparison.OrdinalIgnoreCase)) In++;
                else if (item.TransactionType.Equals("out", StringComparison.OrdinalIgnoreCase)) Out++;
                else if (item.TransactionType.Equals("adj", StringComparison.OrdinalIgnoreCase)) adj++;
            }

            // Pagination
            var totalCount = filteredTransactions.Count;
            var pagedResult = filteredTransactions
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            // Pass parameters to view
            ViewBag.countout = Out;
            ViewBag.countin = In;
            ViewBag.countadj = adj;
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.WareHouseId = WareHouseId;
            ViewBag.WareHouses = warehouseRepo.GetAll().ToList();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)size);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = size;
            ViewBag.TotalCount = totalCount;

            return View(pagedResult);
        }

        public IActionResult GetTransactionById(int id)
        {
            Transaction tr = transactionRepo.GetById(id);
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
            return View(transactionRepo.GetById(id));
        }
        public IActionResult DeleteTransaction(int id)
        {
            return View();
        }
        public IActionResult AddTransaction()
        {
            var viewModel = new AddTransactionViewModel
            {
                Categories = categoryRepo.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList(),

                Warehouses = warehouseRepo.GetAll().Select(w => new SelectListItem
                {
                    Value = w.WareHouseId.ToString(),
                    Text = w.Name
                }).ToList(),
            };
            ViewBag.RecentTransactions = transactionRepo.GetAll().Take(5).ToList();
            ViewBag.CountTransactions  = transactionRepo.GetAll().Count();
            ViewBag.CountWarehouses    = warehouseRepo.GetAll().Count();
            ViewBag.CountProducts      = productRepo.GetAll().Count();

            return View(viewModel);
        }
        public IActionResult SaveAddTransaction(Transaction transaction, int FromWarehouseId, int ToWarehouseId)
        {
            transaction.UserId = "156CF1F8-FB1D-4167-A6B6-9ABC51BA78F5";
            transaction.TransactionDate = DateTime.Now;

                try
                {
                    // Handle different transaction types
                    if (transaction.TransactionType == "In")
                    {
                        // Increase inventory
                        var inventory = inventoryRepo.GetAll()
                            .FirstOrDefault(i => i.ProductId == transaction.ProductId &&
                                                i.WareHouseId == transaction.WareHouseId);
                        var product = productRepo.GetAll()
                            .FirstOrDefault(p => p.ProductId == transaction.ProductId);
                    product.TotalStocQuantity += transaction.QuantityChange;
                    productRepo.Update(product);
                    if (inventory == null)
                        {
                            inventoryRepo.Add(new InventoryItem
                            {
                                ProductId = transaction.ProductId,
                                WareHouseId = transaction.WareHouseId,
                                Quantity = transaction.QuantityChange
                            });
                        }
                        else
                        {
                            inventory.Quantity += transaction.QuantityChange;
                            inventoryRepo.Update(inventory);
                        }
                    }
                    else if (transaction.TransactionType == "Out")
                    {
                        // Decrease inventory
                        var inventory = inventoryRepo.GetAll()
                            .FirstOrDefault(i => i.ProductId == transaction.ProductId &&
                                                i.WareHouseId == transaction.WareHouseId);

                        if (inventory == null || inventory.Quantity < transaction.QuantityChange)
                        {
                            ModelState.AddModelError("", "Not enough stock in the warehouse");
                            return View("AddTransaction", transaction);
                        }

                        inventory.Quantity -= transaction.QuantityChange;
                        inventoryRepo.Update(inventory);
                    }
                    else if (transaction.TransactionType == "Adj")
                    {
                        // Adjustment transaction
                        if (!(FromWarehouseId!=0) || !(ToWarehouseId!=0))
                        {
                            ModelState.AddModelError("", "Both warehouses are required for adjustment");
                            return View("AddTransaction", transaction);
                        }

                        // Decrease from source warehouse
                        var fromInventory = inventoryRepo.GetAll()
                            .FirstOrDefault(i => i.ProductId == transaction.ProductId &&
                                                i.WareHouseId == FromWarehouseId);

                        if (fromInventory == null || fromInventory.Quantity < transaction.QuantityChange)
                        {
                            ModelState.AddModelError("", "Not enough stock in the source warehouse");
                            return View("AddTransaction", transaction);
                        }

                        // Increase in destination warehouse
                        var toInventory = inventoryRepo.GetAll()
                            .FirstOrDefault(i => i.ProductId == transaction.ProductId &&
                                                i.WareHouseId == ToWarehouseId);

                        // Update inventories
                        fromInventory.Quantity -= transaction.QuantityChange;

                        if (toInventory == null)
                        {
                            inventoryRepo.Add(new InventoryItem
                            {
                                ProductId = transaction.ProductId,
                                WareHouseId = ToWarehouseId,
                                Quantity = transaction.QuantityChange
                            });
                        }
                        else
                        {
                            toInventory.Quantity += transaction.QuantityChange;
                            inventoryRepo.Update(toInventory);
                        }

                        inventoryRepo.Update(fromInventory);
                    }

                    // Save transaction
                    transactionRepo.Add(transaction);
                    return RedirectToAction("GetAllTransactions");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving transaction: " + ex.Message);
                }
            


            return View("AddTransaction");
        }
        public IActionResult GetProductsByCategory(int CategoryId)
        {
            var products = productRepo.GetAll()
                .Where(p => p.CategoryId == CategoryId)
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                }).ToList();

            return Json(products);
        }
        public IActionResult GetStock(int productId, int warehouseId)
        {
            var inventory = inventoryRepo.GetAll()
                .FirstOrDefault(i => i.ProductId == productId && i.WareHouseId == warehouseId);

            return Json(inventory?.Quantity ?? 0);
        }
        public IActionResult UpdateTransaction(int id)
        {
            return View();
        }


    }

}
