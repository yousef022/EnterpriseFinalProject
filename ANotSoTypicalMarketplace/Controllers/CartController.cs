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
        public Cart Post(int productId,[FromBody] Cart cart)
        {
            var c = new Cart
            {
                Id = cart.Id
            };

            var prod = _context.Products.First(p=> p.Id == productId);

            cart.CartItems = new List<Product> { new Product { Name = prod.Name, Description = prod.Description, Price = prod.Price, ShippingFee = prod.ShippingFee, IsUsed = prod.IsUsed, CanReturn = prod.CanReturn, CategoryId = prod.CategoryId, CartId = cart.Id} };

            prod.CartId = cart.Id;

           

            _context.Carts.Add(c);
            _context.SaveChanges();

            return cart;
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
            _context.Remove(cart);
            _context.SaveChanges();
        }
    }
}
