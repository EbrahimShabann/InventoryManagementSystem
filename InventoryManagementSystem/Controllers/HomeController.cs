using System.Diagnostics;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork uof;
        private readonly IproductRepo productRepo;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork uof,IproductRepo productRepo)
        {
            _logger = logger;
            this.uof = uof;
            this.productRepo = productRepo;
        }
        

        public IActionResult Index()
        {
            var homeVM = new homeViewModel
            {
                warehousesNum = uof.warehouseRepo.GetAll().Count,
                categoriesNum = uof.categoryRepo.GetAll().Count,
                productsNum = productRepo.GetAll().Count,
                suppliersNum = uof.supplierRepo.GetAll().Count,
                LowStockProducts=productRepo.GetAll().Where(p=>p.TotalStocQuantity<=5),
            }; 
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
