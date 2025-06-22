using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class AttributesConstraintsController : Controller
    {
        public IActionResult CheckUniqueness(string Name)
        {
            return View();
        }
        public IActionResult CheckSKUunique(string SKU)
        {
            return View();
        }
       
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
