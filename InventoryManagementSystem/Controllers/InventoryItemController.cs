using InventoryManagementSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Services.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryItemController : Controller
    {
        private readonly IUnitOfWork uof;
        public InventoryItemController(IUnitOfWork uof)
        {
            this.uof = uof;
        }
        public IActionResult Index(string sortOrder, string searchString, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["WarehouseSortParam"] = sortOrder == "warehouse" ? "warehouse_desc" : "warehouse";
            ViewData["QuantitySortParam"] = sortOrder == "quantity" ? "quantity_desc" : "quantity";

            var inventoryItem = uof.inventoryItemRepo.sort(sortOrder).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                inventoryItem = inventoryItem.Where(s =>
                    s.Product.Name.Contains(searchString) ||
                    (s.WareHouse != null && s.WareHouse.Name.Contains(searchString))
                );
            }

            var pagedResult = inventoryItem.ToPagedResult(page, size);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = size;
            ViewBag.TotalPages = pagedResult.TotalPages;

            return View(pagedResult);
        }
        public IActionResult UpSert(int? id)
        {
            if (id == 0 || id == null) return PartialView(new InventoryItem());
            else
            {
                var inventoryItemFromDb = uof.inventoryItemRepo.GetById(id);
                return PartialView(inventoryItemFromDb);
            }
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
