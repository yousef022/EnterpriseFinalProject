using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Identity;

namespace ANotSoTypicalMarketplace.Services
{
    public class PasswordHasher : IPasswordHasher<User>
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

    }
}
