using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InventoryManagementSystem.ViewModels
{
    public class InventoryItemFormViewModel
    {
        public InventoryItem InventoryItem { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> WareHouses { get; set; }
    }
}