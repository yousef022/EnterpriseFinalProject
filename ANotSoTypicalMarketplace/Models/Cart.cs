//Author: Yousef Osman 

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANotSoTypicalMarketplace.Models
{
    public class Cart
    {


        [Key]
        public int Id { get; set; }

        // This could be a user's identifier to link the cart to a specific user
        public int UserId { get; set; }

        // Navigation property to hold the collection of CartItems
        public virtual List<CartItem> CartItems { get; set; }
    }
}
