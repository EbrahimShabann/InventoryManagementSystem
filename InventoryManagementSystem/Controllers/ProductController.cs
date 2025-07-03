using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        IproductRepo IproductRepo;
        private readonly IUnitOfWork uof;


        public ProductController(IproductRepo repoProduct, IUnitOfWork uof)
        {
            IproductRepo = repoProduct;
            this.uof = uof;
        }

        public IActionResult Index(string searchText, int? categoryId, int? supplierId, int page = 1, int pageSize = 9)
        {
            var products = IproductRepo.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                products = products.Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText));
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (supplierId.HasValue)
            {
                products = products.Where(p => p.SupplierId == supplierId);
            }

            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling(products.Count() / (double)pageSize);
            ViewBag.CurrentPage = page;

            ViewBag.Categoriey = uof.categoryRepo.GetAll();
            ViewBag.Supplier = uof.supplierRepo.GetAll();

            ViewBag.SearchText = searchText;
            ViewBag.CategoryId = categoryId;
            ViewBag.SupplierId = supplierId;

            return View("Index", pagedProducts);
        }
        public IActionResult New()
        {
            ViewBag.Categoriey = uof.categoryRepo.GetAll();
            ViewBag.Supplier = uof.supplierRepo.GetAll();

            return View("New");
        }
        [HttpPost]
        public IActionResult SaveNew(Product productRequest ,IFormFile Image)
        {
            if (ModelState.IsValid == true)

            {
                if (Image != null && Image.Length > 0)
                {
                    string fileName = Path.GetFileName(Image.FileName);
                    string finalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products", fileName);

                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    productRequest.Image = fileName;
                }
                IproductRepo.Add(productRequest);
                IproductRepo.save();
                return RedirectToAction("Index");

            }
            ViewBag.Categoriey = uof.categoryRepo.GetAll();
            ViewBag.Supplier = uof.supplierRepo.GetAll();

            return View("New", productRequest);
        }
        public IActionResult edit(int id)
        {
            var prd = IproductRepo.GetById(id);
            ViewBag.Categoriey = uof.categoryRepo.GetAll();
            ViewBag.Supplier = uof.supplierRepo.GetAll();

            if (prd == null)
            {
                return NotFound();
            }

            return View("edit", prd);


        }
        [HttpPost]
        public IActionResult SaveEdit(int id, Product ProductRequest, IFormFile Image)
        {
            var prd = IproductRepo.GetById(id);
            if (prd == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                prd.Name = ProductRequest.Name;
                prd.Description = ProductRequest.Description;
                prd.SupplierId = ProductRequest.SupplierId;
                prd.CategoryId = ProductRequest.CategoryId;
                prd.Price = ProductRequest.Price;
                prd.TotalStocQuantity = ProductRequest.TotalStocQuantity;
                prd.ReorderLevel = ProductRequest.ReorderLevel;

                if (Image != null && Image.Length > 0)
                {
                    string fileName = Path.GetFileName(Image.FileName);
                    string finalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products", fileName);

                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    prd.Image = fileName;
                }

                IproductRepo.save();
                return RedirectToAction("Index");
            }
            ViewBag.Categoriey = uof.categoryRepo.GetAll();
            ViewBag.Supplier = uof.supplierRepo.GetAll();

            return View("Modify", ProductRequest);
        }

        public IActionResult Delete(int id)

        {
            var crs = IproductRepo.GetById(id);
            IproductRepo.Delete(id);
            IproductRepo.save();
            return RedirectToAction("Index");
        }




    }
}
