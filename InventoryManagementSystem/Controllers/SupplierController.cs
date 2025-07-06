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

        public IActionResult Index(string sortOrder, string searchString, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["EmailSortParam"] = sortOrder == "email" ? "email_desc" : "email";
            ViewData["CurrentFilter"] = searchString;

            var suppliers = uof.supplierRepo.sort(sortOrder).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s =>
                    s.Name.Contains(searchString) ||
                    (s.Email != null && s.Email.Contains(searchString))
                );
            }

            var pagedResult = suppliers.ToPagedResult(page, size);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = size;
            ViewBag.TotalPages = pagedResult.TotalPages;

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
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = StaticDetails.Admin_Role)]
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
                        // add new supplier
                        uof.supplierRepo.Add(supplier);
                    }
                    else
                    {
                        //update existed supplier
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
