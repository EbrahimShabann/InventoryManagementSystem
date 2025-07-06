using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class InventoryItemController : Controller
    {
        IproductRepo productRepo;

        private readonly IUnitOfWork uof;
        public InventoryItemController(IproductRepo repoProduct, IUnitOfWork uof)
        {
            productRepo = repoProduct;
            this.uof = uof;
        }
        public IActionResult Index(string sortOrder, string searchString, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["WarehouseSortParam"] = sortOrder == "warehouse" ? "warehouse_desc" : "warehouse";
            ViewData["QuantitySortParam"] = sortOrder == "quantity" ? "quantity_desc" : "quantity";
            ViewData["CurrentFilter"] = searchString;

            var inventoryItems = uof.inventoryItemRepo.sort(sortOrder).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                inventoryItems = inventoryItems.Where(i =>
                    i.Product.Name.Contains(searchString) ||
                    i.WareHouse.Name.Contains(searchString)
                );
            }

            var pagedResult = inventoryItems.ToPagedResult(page, size);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = size;
            ViewBag.TotalPages = pagedResult.TotalPages;

            return View(pagedResult);
        }

        public IActionResult UpSert(int? id)
        {
            var products = productRepo.GetAll()
                .Select(p => new SelectListItem { Value = p.ProductId.ToString(), Text = p.Name });

            var warehouses = uof.warehouseRepo.GetAll()
                .Select(w => new SelectListItem { Value = w.WareHouseId.ToString(), Text = w.Name });

            InventoryItem item = id == null || id == 0
                ? new InventoryItem { LastUpdated = DateTime.Now }
                : uof.inventoryItemRepo.GetById(id);

            var viewModel = new InventoryItemFormViewModel
            {
                InventoryItem = item,
                Products = products,
                WareHouses = warehouses
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpSert(InventoryItem inventoryItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(inventoryItem);
                }
                else
                {
                    if (inventoryItem.InventoryItemId == 0)
                    {
                        // add new inventory item
                        uof.inventoryItemRepo.Add(inventoryItem);
                    }
                    else
                    {
                        //update existed inventory item
                        uof.inventoryItemRepo.Update(inventoryItem);
                    }
                    uof.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ex", ex.Message);
                return PartialView(inventoryItem);
            }
        }
        public IActionResult Details(int InventoryItemId)
        {
            if (InventoryItemId == 0) return View("Error");
            var inventoryItem = uof.inventoryItemRepo.GetById(InventoryItemId);
            return View(inventoryItem);
        }

        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                uof.inventoryItemRepo.Delete(id);
                uof.Save();
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}