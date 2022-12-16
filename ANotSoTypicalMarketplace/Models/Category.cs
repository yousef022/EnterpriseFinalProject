//Author: Tyler Makris

using System.ComponentModel.DataAnnotations;

namespace ANotSoTypicalMarketplace.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
