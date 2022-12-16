//Author: Yousef Osman
using System.ComponentModel.DataAnnotations;

namespace ANotSoTypicalMarketplace.Models
{
    public class PriceMatch
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }


    }
}
