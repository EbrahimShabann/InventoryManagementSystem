using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        public int TransactionId { get; set; }


        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [Remote("CheckTransType", "AttributesConstraints")]
        [MaxLength(20,ErrorMessage = "Transaction Type Max length is 20")]
        public string TransactionType { get; set; }


        [Required]
        [Remote("ChechQuantityChange", "AttributesConstraints")]
        public int QuantityChange { get; set; }


        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int WareHouseId { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual WareHouse WareHouse { get; set; }
    }

    public enum TransactionType
    {
        In,
        Out,
        Adjustment

    }
}
