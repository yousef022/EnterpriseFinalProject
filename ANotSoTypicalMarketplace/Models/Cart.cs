using System.ComponentModel.DataAnnotations;

namespace ANotSoTypicalMarketplace.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Added { get; set; }
        [Required]
        public List<Product> CartItems { get; set; }

    }
}
