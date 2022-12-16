//Author: Yousef Osman

using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ANotSoTypicalMarketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Database _context;
        public CartController(Database context)
        {
            _context= context;
        }

        [HttpGet]
        public IEnumerable<Cart> Get() => _context.Carts.Include(x => x.CartItems).ToList();

        [HttpPost("{productId}")]
        public Cart Post(int productId)
        {

            var prod = _context.Products.First(p=> p.Id == productId);
           

            var c = new Cart
            {
                Id = productId,
                Added = true
            };

            prod.CartId= c.Id;


            _context.Carts.Add(c);
            _context.SaveChanges();
            return c;
        

        }


        [HttpPut]
        public Cart Put([FromBody] Cart cart)
        {
            var c = _context.Carts.Find(cart.Id);

            c.CartItems = cart.CartItems;

            return cart;

        }


        [HttpDelete("{id}")]
        public void Delete(int id) 
        {
            var cart = _context.Carts.Find(id);
            var prod = _context.Products.Find(id);

            prod.CartId = null;

            _context.Remove(cart);
            _context.SaveChanges();
        }
    }
}
