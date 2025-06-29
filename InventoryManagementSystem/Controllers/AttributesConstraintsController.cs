using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AttributesConstraintsController : Controller
    {
        private readonly AppDbContext db;

        public AttributesConstraintsController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult CheckWHNameUniq(string Name ,int WareHouseId)
        {
            //check uniqness of warehouse name 
            var ware = db.WareHouses.FirstOrDefault(w => w.Name == Name);
            if (ware != null && WareHouseId!=ware.WareHouseId)   // in case adding new one only or update another ware with name that is existed already
            {
                return Json("This wareHouse is already existed");
            }
            return Json(true);

        }
        public IActionResult CheckCatNameUniq(string Name,int CategoryId)
        {
            //check uniqness of category name 
            var cat = db.Categories.FirstOrDefault(w => w.Name == Name);
            if (cat != null && CategoryId == 0)
            {
                return Json("This Category is already existed");
            }
            return Json(true);

        }
        //public IActionResult CheckSKUunique(string SKU)
        //{
        //    return View();
        //}

        public IActionResult CheckTransType(string TransactionType)
        {
            if(TransactionType.ToLower()=="in" || TransactionType.ToLower() == "out" 
                || TransactionType.ToLower() == "adjustment")
            {
                return Json(true);
            }
            return Json("No Valid Transaction Type");
        }
       
        public IActionResult ChechQuantityChange(int QuantityChange)
        {
            if(QuantityChange!=0)
            {
                return Json(true);
            }
            return Json("No Change in quantity");
        }
       
    }
}
