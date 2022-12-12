﻿using System.ComponentModel.DataAnnotations;

namespace ANotSoTypicalMarketplace.Models
{
    public class Product
    {
        [Required]
        int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double ShippingFee { get; set; }
        [Required]
        public bool IsUsed { get; set; }
        [Required]
        public bool CanReturn { get; set; }
        [Required]
        public int CategoryId { get; set; }



        

    }
}