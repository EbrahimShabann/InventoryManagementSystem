using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork uof;
        public CategoryController(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        public IActionResult Index(string sortOrder, string searchString, int page = 1, int size = 10)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["CurrentFilter"] = searchString;

            var categories = uof.categoryRepo.Sort(sortOrder).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.Name.Contains(searchString) || (c.Description != null && c.Description.Contains(searchString)));
            }
            var totalCount = categories.Count();
            var pagedResult = categories.Skip((page - 1) * size).Take(size).ToList();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)size);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = size;
            ViewBag.TotalCount = totalCount;
            return View(pagedResult);
        }

        public IActionResult Add()
        {
            return PartialView("UpSert", new Category());
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("UpSert", category);
            }

            if (uof.categoryRepo.GetAll().Any(c => c.Name == category.Name))
            {
                return Json(new { success = false, error = "Category name already exists." });
            }

            uof.categoryRepo.Add(category);
            uof.Save();
            return Json(new { success = true });
        }

        public IActionResult Edit(int id)
        {
            var category = uof.categoryRepo.GetById(id);
            if (category == null) return PartialView("Error");
            return PartialView("UpSert", category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("UpSert", category);
            }

            if (uof.categoryRepo.GetAll().Any(c => c.Name == category.Name && c.CategoryId != category.CategoryId))
            {
                return Json(new { success = false, error = "Category name already exists." });
            }

            uof.categoryRepo.Update(category);
            uof.Save();
            return Json(new { success = true });
        }

        public IActionResult Delete(int id)
        {
            var category = uof.categoryRepo.GetById(id);
            if (category == null) return PartialView("Error");
            return PartialView(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            uof.categoryRepo.Delete(id);
            uof.Save();
            return Json(new { success = true });
        }

        public IActionResult Details(int CategoryId)
        {
            if (CategoryId == 0) return PartialView("Error");
            var category = uof.categoryRepo.GetById(CategoryId);
            return PartialView(category);
        }
    }
}