//Author: Ammar Khan
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

        public UserController(Database context)
        {
            _context = context;
        }

       


        [HttpGet]
        public IEnumerable<User> Get() => _context.Users.Include(x => x.Products).ToList();

        /*[HttpPost]
        public User Post([FromBody] User user)
        {
            var u = new User
            {
                //Id = 1,
                UserName = user.UserName,
                FullName = user.FullName,
                UserEmail = user.UserEmail,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                IsBanned= false,
                Products = null
            };

            _context.Users.Add(u);

            _context.SaveChanges();

            return user;
        }*/

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            // No need to set Id; it will be generated automatically

            user.IsBanned = false;
            user.Products = null; 
            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding data to the database: {ex.Message}");
            }

            return Ok(user); // Now user contains the auto-generated Id
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
