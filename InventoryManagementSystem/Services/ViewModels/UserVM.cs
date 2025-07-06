using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Services.ViewModels
{
    public class UserVM
    {

        public ApplicationUser ApplicationUser { get; set; }
       
        [Required]
        [MinLength(7,ErrorMessage ="Full Name Must be at least 7 chars")]
        [MaxLength(50,ErrorMessage = "Full Name can't be more than 50 chars")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{2,19}$",
            ErrorMessage = "Username must start with a letter, can contain letters, numbers, and underscores, and be between 3 and 20 characters long.")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?:\+20|0020|0)?1[0125]\d{8}$",
          ErrorMessage = "Phone Number must be in one of these formats: +20xxxxxxxxxx, 0020xxxxxxxxxx, or 0xxxxxxxxxx")]
        public string PhoneNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
