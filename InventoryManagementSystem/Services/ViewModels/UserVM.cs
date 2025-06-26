using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSystem.Services.ViewModels
{
    public class UserVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
