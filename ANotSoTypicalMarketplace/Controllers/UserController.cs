using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ANotSoTypicalMarketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Database _context;

        [HttpGet]
        public IEnumerable<User> Get() => _context.Users.Include(x => x.Products).ToList();

        [HttpPost]
        public User Post([FromBody] User user)
        {
            var u = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                UserEmail = user.UserEmail,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                IsBanned= user.IsBanned,
            };

            _context.Users.Add(u);
            _context.SaveChanges();

            return user;
        }

        [HttpPut]
        public User Put([FromBody] User user)
        {
            var usr = _context.Users.First(u => u.Id == user.Id);

            usr.UserName = user.UserName;
            usr.FullName = user.FullName;
            usr.UserEmail= user.UserEmail;
            usr.Password = user.Password;
            usr.PhoneNumber= user.PhoneNumber;
            usr.IsBanned= user.IsBanned;

            _context.SaveChanges();

            return usr;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usr = _context.Users.First(u => u.Id == id);
            _context.Remove(usr);
            _context.SaveChanges();
        }

    }
}
