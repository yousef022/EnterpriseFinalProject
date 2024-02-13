//Author: Yousef Osman

using ANotSoTypicalMarketplace.DTO;
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
            _context = context;
        }


        //[HttpGet]
        //public IEnumerable<Cart> Get() => _context.Carts.Include(x => x.CartItems).ToList();

        //GET endpoint
        [HttpGet("{userId}")]
        public ActionResult<Cart> Get(int userId)
        {

            try
            {
                var cart = _context.Carts
                       .Include(c => c.CartItems)
                       .ThenInclude(ci => ci.Product)
                       .FirstOrDefault(c => c.UserId == userId);


                return cart;
            }
            catch(Exception) 
            {
                return StatusCode(500);
            }
            
        }
        

        //POST endpoint
        [HttpPost]
        public ActionResult<CartItem> Post(AddToCartDto addToCartDto)
        {
            try
            {
                Console.WriteLine(addToCartDto.UserId);
                var product = _context.Products.Find(addToCartDto.ProductId);

                var cart = _context.Carts.FirstOrDefault(c => c.UserId == addToCartDto.UserId); //Is Null for some reason

                var cartItem = new CartItem { CartId = cart.Id, ProductId = addToCartDto.ProductId };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();

                return cartItem;
            }
            catch (Exception) 
            {
                return StatusCode(500);
            }
        }


        //PUT endpoint
        [HttpPut("{cartItemId}")]
        public IActionResult Put(int cartItemId, CartItem updatedCartItem)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem == null) return NotFound("CartItem not found.");

            //cartItem.Quantity = updatedCartItem.Quantity; // Assuming there's a Quantity property to update
            _context.Entry(cartItem).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        //DELETE endpoint
        [HttpDelete("{cartItemId}")]
        public IActionResult Delete(int cartItemId)
        {
            var cartItem = _context.CartItems.First(ci => ci.Id == cartItemId);
            if (cartItem == null) return NotFound("CartItem not found.");

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return NoContent();
        }





        //[HttpGet]
        //public IEnumerable<Cart> Get() => _context.Carts.Include(x => x.CartItems).ToList();

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var cartItems = _context.CartItems
        //        .Include(ci => ci.Product) // Include the Product details for each CartItem
        //        .ToList();

        //    if (cartItems == null || !cartItems.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cartItems);
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var carts = _context.Carts
        //        .Include(c => c.CartItems)
        //            .ThenInclude(ci => ci.Product) // Include products within cart items
        //        .ToList();

        //    if (!carts.Any())
        //    {
        //        return NotFound("No carts found.");
        //    }

        //    return Ok(carts);
        //}



        //[HttpPost("{productId}")]
        //public IActionResult Post(int productId)
        //{

        //    var prod = _context.Products.FirstOrDefault(p=> p.Id == productId);
        //    if (prod == null)
        //    {
        //        return NotFound();
        //    }


        //    var cartItem = new CartItem
        //    {
        //        //Id = productId,
        //        //Added = true
        //        //ProductId = productId.ToString()
        //        ProductId = productId

        //    };

        //    _context.CartItems.Add(cartItem);
        //    _context.SaveChanges();


        //    //_context.Carts.Add(c);
        //    //_context.SaveChanges();

        //    //prod.CartId = c.Id;
        //    //_context.SaveChanges();

        //    //return c;

        //    return Ok();


        //}

        //Unused for now

        //[HttpPut("{cartId}")]
        //public IActionResult Put(int cartId, [FromBody] CartItem updateCartItem)
        //{
        //    // Find the existing cart item
        //    var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id == updateCartItem.Id && ci.CartId == cartId);
        //    if (cartItem == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update the cart item details
        //    cartItem.Quantity = updateCartItem.Quantity;
        //    // Update other fields as needed

        //    _context.CartItems.Update(cartItem);
        //    _context.SaveChanges();

        //    return Ok(cartItem);
        //}


        //[HttpDelete("{cartItemId}")]
        //public IActionResult Delete(int cartItemId)
        //{
        //    var cartItem = _context.CartItems.Find(cartItemId);
        //    if (cartItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CartItems.Remove(cartItem);
        //    _context.SaveChanges();

        //    return NoContent(); // 204 No Content is standard for a successful delete response
        //}



        //[HttpPut]
        //public Cart Put([FromBody] Cart cart)
        //{
        //    var c = _context.Carts.Find(cart.Id);

        //    c.CartItems = cart.CartItems;

        //    return cart;

        //}


        //[HttpDelete("{id}")]
        //public void Delete(int id) 
        //{
        //    var cart = _context.Carts.Find(id);
        //    var prod = _context.Products.Find(id);

        //    prod.CartId = null;

        //    _context.Remove(cart);
        //    _context.SaveChanges();
        //}
    }
}
