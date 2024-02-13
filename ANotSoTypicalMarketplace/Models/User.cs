
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ANotSoTypicalMarketplace.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username should not be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name should not be empty")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email address should not be empty")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number should not be empty")]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}", ErrorMessage = "Phone number is in wrong format")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsBanned { get; set; }

        // Navigation property for Cart. Each user has one Cart.
        public virtual Cart? Cart { get; set; }

        //[AllowNull]
        //public List<Product>? Products { get; set; }
    }
}
