using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WareHouseController : Controller
    {

        public WareHouseController(IRepository<WareHouse> whRepo)
        {
            WhRepo = whRepo;
        }

        public IRepository<WareHouse> WhRepo { get; }

        public IActionResult Index(int page=1, int size=10)
        {
            var wareHouses = WhRepo.GetAll().AsEnumerable();
            var pagedResult = wareHouses.ToPagedResult(page, size);
            return View(pagedResult);
        }
    }
}
