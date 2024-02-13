//Author: Ammar Khan
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ANotSoTypicalMarketplace.Services;
using NetDevPack.Security.PasswordHasher;

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


        //[HttpGet]
        //public IEnumerable<User> Get() => _context.Users.Include(x => x.Products).ToList();
        
        
        //GET endpoint
        [HttpGet]
        public IEnumerable<User> Get() => _context.Users.Include(u => u.Cart).ToList();


        //POST endpoint
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user) 
        {
            try
            {
                // Hash password
                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(user,user.Password);

               
                //Initialize the User
                var us = new User 
                {
                    UserName = user.UserName,
                    FullName = user.FullName,
                    UserEmail = user.UserEmail,
                    Password = hashedPassword,
                    PhoneNumber = user.PhoneNumber,
                    IsBanned = false
                    //Cart = newCart
                };

                _context.Users.Add(us);
                _context.SaveChanges();

                // Create the cart for user
                var newCart = new Cart
                {
                    UserId = us.Id
                };

                _context.Carts.Add(newCart);
                _context.SaveChanges();

                // Find user by ID
                var use = _context.Users.FirstOrDefault(u => u.Id == us.Id);

                //Assign only 1 cart to the user
                use.UserName = user.UserName;
                use.FullName = user.FullName;
                use.UserEmail = user.UserEmail;
                use.Password = hashedPassword;
                use.PhoneNumber = user.PhoneNumber;
                use.IsBanned = user.IsBanned;
                use.Cart = newCart;

            
                _context.SaveChanges();

                return use;
            }
            catch (Exception) 
            {
                return NotFound();
            }

        }


        //PUT endpoint

        [HttpPut]
        public ActionResult<User> Put([FromBody] User user)
        {
            try
            {
                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(user, user.Password);
                
                var use = _context.Users.FirstOrDefault(u => u.Id == user.Id);

                use.UserName = user.UserName;
                use.FullName = user.FullName;
                use.UserEmail = user.UserEmail;
                use.Password = hashedPassword;
                use.PhoneNumber = user.PhoneNumber;
                use.IsBanned = user.IsBanned;

                _context.Users.Add(use);
                _context.SaveChanges();

                return use;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }



        //DELETE endpoint
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                var user = _context.Users.First(u => u.Id == u.Id);
                _context.Remove(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }


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

        //[HttpPost]
        //public ActionResult<User> Post([FromBody] User user)
        //{
        //    // No need to set Id; it will be generated automatically

        //    user.IsBanned = false;
        //    user.Products = null; 
        //    _context.Users.Add(user);

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding data to the database: {ex.Message}");
        //    }

        //    return Ok(user); // Now user contains the auto-generated Id
        //}



        //[HttpPut]
        //public User Put([FromBody] User user)
        //{
        //    var usr = _context.Users.First(u => u.Id == user.Id);

        //    usr.UserName = user.UserName;
        //    usr.FullName = user.FullName;
        //    usr.UserEmail= user.UserEmail;
        //    usr.Password = user.Password;
        //    usr.PhoneNumber= user.PhoneNumber;
        //    usr.IsBanned= user.IsBanned;

        //    _context.SaveChanges();

        //    return usr;
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    var usr = _context.Users.First(u => u.Id == id);
        //    _context.Remove(usr);
        //    _context.SaveChanges();
        //}

    }
}
