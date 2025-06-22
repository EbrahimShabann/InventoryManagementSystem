using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Product")]
    public class Product
    {

        public int ProductId { get; set; }



        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }


        [Required]
        [MaxLength(200, ErrorMessage = "Product Name Max Length is 200 Chars")]
        public string Name { get; set; }


        [MaxLength(1000, ErrorMessage = "Description Max Length is 1000 Chars")]
        public string Description { get; set; }


        [Required]
        [Remote("CheckSKUunique", "AttributesConstraints")]
        [MaxLength(50, ErrorMessage = "SKU Max Length is 200 Chars")]
        public string SKU { get; set; }


        [Required]
        [Range(0,double.MaxValue,ErrorMessage = " Price can't be less than 0")]
        public double Price { get; set; }


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = " Reorder Level can't be less than 0")]
        public int ReorderLevel { get; set; }


        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


        [Required]
        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Current Stock Quantity can't be less than 0")]
        public int CurrentStockQuantity { get; set; }


        public virtual List<Transaction> Transactions { get; set; }
    }
}
