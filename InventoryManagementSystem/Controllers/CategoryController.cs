using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
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
            var pagedItems = categories.Skip((page - 1) * size).Take(size).ToList();
            var pagedResult = new PagedResult<Category>
            {
                Items = pagedItems,
                TotalItems = categories.ToList(),
                TotalItemsCount = totalCount,
                PageNumber = page,
                PageSize = size
            };
            return View(pagedResult);
        }

        // Unified UpSert for Add/Edit (GET)
        public IActionResult UpSert(int? id)
        {
            if (id == null || id == 0)
                return PartialView("UpSert", new Category());
            var category = uof.categoryRepo.GetById(id);
            if (category == null) return PartialView("Error");
            return PartialView("UpSert", category);
        }

        // Unified UpSert for Add/Edit (POST)
        [HttpPost]
        public IActionResult UpSert(Category category)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("UpSert", category);
            }

            try
            {
                // Check for duplicate name
                if (uof.categoryRepo.GetAll().Any(c =>
                    c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase) &&
                    c.CategoryId != category.CategoryId))
                {
                    ModelState.AddModelError("Name", "Category name already exists.");
                    return PartialView("UpSert", category);
                }

                if (category.CategoryId == 0)
                {
                    uof.categoryRepo.Add(category);
                }
                else
                {
                    var existingCategory = uof.categoryRepo.GetById(category.CategoryId);
                    if (existingCategory == null)
                    {
                        return Content("error: Category not found");
                    }

                    existingCategory.Name = category.Name;
                    existingCategory.Description = category.Description;
                    uof.categoryRepo.Update(existingCategory);
                }

                uof.Save();
                return Content("success");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error saving category: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving. Please try again.");
                return PartialView("UpSert", category);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = uof.categoryRepo.GetById(id);
            if (category == null)
            {
                return Content("error");
            }

            // Check if category has associated products
            if (category.Products != null && category.Products.Any())
            {
                return Content("has_products");
            }

            try
            {
                uof.categoryRepo.Delete(id);
                uof.Save();
                return Content("success");
            }
            catch
            {
                return Content("error");
            }
        }

        public IActionResult SortTable(string sortOrder)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "name_desc" : "name";

            var categories = uof.categoryRepo.Sort(sortOrder).AsQueryable();
            var pagedResult = new PagedResult<Category>
            {
                Items = categories.ToList(),
                TotalItems = categories.ToList(),
                TotalItemsCount = categories.Count(),
                PageNumber = 1,
                PageSize = categories.Count()
            };

            return PartialView("sortTable", pagedResult);
        }

        public IActionResult Search(string searchText)
        {
            var categories = uof.categoryRepo.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                categories = categories.Where(c => c.Name.Contains(searchText) ||
                             (c.Description != null && c.Description.Contains(searchText)));
            }

            var pagedResult = new PagedResult<Category>
            {
                Items = categories.ToList(),
                TotalItems = categories.ToList(),
                TotalItemsCount = categories.Count(),
                PageNumber = 1,
                PageSize = categories.Count()
            };

            return PartialView("sortTable", pagedResult);
        }
    }
}