using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Supplier")]
    public class Supplier
    {

      
        public int SupplierId { get; set; }

        [Required]
        [Remote("CheckUniqueness", "AttributesConstraints")]
        [MaxLength(200, ErrorMessage = "Supplier Name Max Length is 200 Chars")]
        public string Name { get; set; }



        [MaxLength(100, ErrorMessage = "Contact Person Max Length is 100 Chars")]
        public string ContactPerson { get; set; }



        [DataType(DataType.PhoneNumber,ErrorMessage ="InValid Phone Number")]
        [MaxLength(20, ErrorMessage = "Phone Max Length is 20 Chars")]
        public string Phone { get; set; }



        [DataType(DataType.EmailAddress,ErrorMessage ="InValid Email")]
        public string Email { get; set; }



        [MaxLength(500, ErrorMessage = "Address Max Length is 500 Chars")]
        public string Address { get; set; }


        public virtual List<Product> Products { get; set; }
    }
}
