using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles =StaticDetails.Admin_Role +","+StaticDetails.Manager_Role)]
    public class WareHouseController : Controller
    {
        private readonly IUnitOfWork uof;

        public WareHouseController(IUnitOfWork uof)
        {

            this.uof = uof;
        }



        public IActionResult Index(int page = 1, int size = 10)
        {
           
            var wareHouses = uof.warehouseRepo.sort("",User); // Default sort by ID
            var pagedResult = wareHouses.ToPagedResult(page, size);
            return View(pagedResult);
        }

        public IActionResult sortTable(string sortOrder, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["locationSortParam"] = sortOrder == "loca" ? "loca_desc" : "loca";
            ViewData["phoneSortParam"] = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewData["productsSortParam"] = sortOrder == "products" ? "products_desc" : "products";
            ViewData["dateSortParam"] = sortOrder == "date" ? "date_desc" : "date";

            var wareHouses = uof.warehouseRepo.sort(sortOrder,User).AsEnumerable();
            ViewBag.sortOrder = sortOrder;
            var pagedResult = wareHouses.ToPagedResult(page, size);
            return PartialView("sortTable", pagedResult);

        }
        public IActionResult Details(int WareHouseId)
        {
            if (WareHouseId == 0)
                return View("Error");

            var ware = uof.warehouseRepo.GetById(WareHouseId);

            if (ware == null)
                return View("Error");

            // Use a list to store all warnings and show them together
            List<string> lowStockWarnings = new();
            List<string> EmptyStockWarnings = new();

            foreach (var item in ware.InventoryItems)
            {
                if (item?.Quantity== 0)
                {
                    TempData["error"]=($" Product: {item.Product.Name} stock is empty!");
                }
                else if (item?.Quantity<= 5)
                {
                    TempData["warning"] = ($" Product: {item.Product.Name} is about to run out!");
                }
            }

            //if (lowStockWarnings.Any())
            //{
            //    TempData["warning"] = string.Join("<br/>", lowStockWarnings);
            //}
            //else if (EmptyStockWarnings.Any())
            //{
            //    TempData["error"] = string.Join("<br/>", EmptyStockWarnings);

            //}

            return View(ware);
        }


        [HttpGet]
        [Authorize(Roles =StaticDetails.Admin_Role)]
        public IActionResult UpSert(int? id)
        {
            ViewBag.Managers = uof.AppUserRepo.GetAllManagers();
            if (id == 0 || id == null) return PartialView(new WareHouse());
            else
            {
                var wareHouseFromDb = uof.warehouseRepo.GetById(id);
                return PartialView(wareHouseFromDb);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StaticDetails.Admin_Role)]

        public IActionResult UpSert(WareHouse ware)
        {
            ViewBag.Managers = uof.AppUserRepo.GetAllManagers();

            try
            {
                if (!ModelState.IsValid)
                {

                    return PartialView(ware);
                }
                else
                {
                    if (ware.WareHouseId == 0)
                    {
                        // add new warehouse
                        uof.warehouseRepo.Add(ware);
                    }
                    else
                    {
                        //update existed warehouse
                        uof.warehouseRepo.Update(ware);
                    }
                    uof.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ex", ex.InnerException.Message);
                return PartialView(ware);
            }
        }


        [Authorize(Roles = StaticDetails.Admin_Role)]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                uof.warehouseRepo.Delete(id);
                uof.Save();
                return RedirectToAction("Index");
            }
            return View("Error");

        }
    }
}
