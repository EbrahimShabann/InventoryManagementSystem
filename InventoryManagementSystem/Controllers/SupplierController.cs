using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork uof;
        public SupplierController(IUnitOfWork uof)
        {
            this.uof = uof;
        }
        public IActionResult Index(string sortOrder, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["EmailSortParam"] = sortOrder == "email" ? "email_desc" : "email";

            var suppliers = uof.supplierRepo.sort(sortOrder).AsEnumerable();
            var pagedResult = suppliers.ToPagedResult(page, size);
            return View(pagedResult);
        }

        public IActionResult UpSert(int? id)
        {
            if (id == 0 || id == null) return PartialView(new Supplier());
            else
            {
                var supplierFromDb = uof.supplierRepo.GetById(id);
                return PartialView(supplierFromDb);
            }
        }
        [HttpPost]
        public IActionResult UpSert(Supplier supplier)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(supplier);
                }
                else
                {
                    if (supplier.SupplierId == 0)
                    {
                        uof.supplierRepo.Add(supplier);
                    }
                    else
                    {
                        uof.supplierRepo.Update(supplier);
                    }
                    uof.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Ex", ex.Message);
                return PartialView(supplier);
            }
        }

        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                uof.supplierRepo.Delete(id);
                uof.Save();
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        public IActionResult Details(int SupplierId)
        {
            if (SupplierId == 0) return View("Error");
            var supplier = uof.supplierRepo.GetById(SupplierId);
            return View(supplier);
        }
    }
}
