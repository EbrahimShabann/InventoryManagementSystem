using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Category")]
    public class Category
    {
       
        public int CategoryId { get; set; }
        [Required]
        [Remote("CheckUniqueness", "AttributesConstraints")]
        [MaxLength(100,ErrorMessage = "Category Name Max Length is 100 Chars")]
         public string Name { get; set; }

        [MaxLength(500,ErrorMessage ="Description Can't exceed 500 chars")]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
