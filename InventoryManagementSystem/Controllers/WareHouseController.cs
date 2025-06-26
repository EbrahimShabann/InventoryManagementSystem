using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;

namespace InventoryManagementSystem.Controllers
{
    public class WareHouseController : Controller
    {
        private readonly IUnitOfWork uof;

        public WareHouseController(IUnitOfWork uof)
        {
            
            this.uof = uof;
        }



        public IActionResult Index(int page = 1, int size = 10)
        {
            var wareHouses = uof.warehouseRepo.sort(""); // Default sort by ID
            var pagedResult = wareHouses.ToPagedResult(page, size);
            return View(pagedResult);
        }

        public IActionResult sortTable(string sortOrder, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["locationSortParam"] = sortOrder == "loca" ? "loca_desc" : "loca";
            ViewData["phoneSortParam"] = sortOrder == "phone" ? "phone_desc" : "phone";

            var wareHouses = uof.warehouseRepo.sort(sortOrder).AsEnumerable();
            ViewBag.sortOrder = sortOrder;
            var pagedResult = wareHouses.ToPagedResult(page, size);
            return PartialView("sortTable", pagedResult);

        }
        public IActionResult Details(int WareHouseId)
        {
            if(WareHouseId ==0 ) return View("Error");
            var ware = uof.warehouseRepo.GetById(WareHouseId);
            return View(ware);
        }

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            if (id == 0 || id == null) return PartialView(new WareHouse());
            else
            {
                var wareHouseFromDb = uof.warehouseRepo.GetById(id);
                return PartialView(wareHouseFromDb); 
            }
        }


        [HttpPost]
        public IActionResult UpSert(WareHouse ware)
        {
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
                ModelState.AddModelError("Ex",ex.Message);
                return PartialView(ware);
            }
        }

        public IActionResult Delete(int id) 
        { 
            if(id!=0)
            {
                uof.warehouseRepo.Delete(id);
                uof.Save();
                return RedirectToAction("Index");
            }
            return View("Error");
        
        }
    }
}
