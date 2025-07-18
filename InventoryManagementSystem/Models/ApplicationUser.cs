﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public bool IsActive { get; set; }


        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }


        [NotMapped]
        [Required]
        public string Role { get; set; }

        


        [Required]
        [MinLength(20,ErrorMessage ="Address must be greater than 20 chars")]
        public string Address { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<WareHouse> WareHouses { get; set; }
    }
}
