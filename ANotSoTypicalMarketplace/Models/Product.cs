//Author: Tyler Makris
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANotSoTypicalMarketplace.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
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

        [ForeignKey("CartId")]
        public int? CartId { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        



        

    }
}
