using InventoryManagementSystem.Repositories.IRepositories;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WareHouseController : Controller
    {
        private readonly IWareHouseRepository wareHouseRepository;

        public WareHouseController(IWareHouseRepository wareHouseRepository)
        {
            this.wareHouseRepository = wareHouseRepository;
        }
        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            var wareHouses = wareHouseRepository.GetAll().AsEnumerable();
            var pagedResult = wareHouses.ToPagedResult(pageNumber, pageSize);
            return View(pagedResult);
        }
    }
}
