using System.ComponentModel.DataAnnotations;

namespace ANotSoTypicalMarketplace.Models
{
    public class User
    {
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
        public int PhoneNumber { get; set; }

        [Required]
        public bool IsBanned { get; set; }

        [Required]
        public List<Product> Products { get; set; }
    }
}
