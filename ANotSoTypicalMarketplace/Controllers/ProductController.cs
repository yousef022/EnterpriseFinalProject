//Author: Tyler Makris
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ANotSoTypicalMarketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Database _context;


        public ProductController(Database context)
        {
            _context = context;
        }


        //GET endpoint
        [HttpGet]
        public IEnumerable<Product> Get() => _context.Products.ToList();


        //POST endpoint
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            try
            {
                var p = new Product
                {   
                    Name = product.Name,
                    ShippingFee = product.ShippingFee,  
                    Price = product.Price,
                    Description = product.Description,
                    Stock = product.Stock,
                    IsUsed = product.IsUsed,
                    CanReturn = product.CanReturn
                };

                _context.Products.Add(p);
                _context.SaveChanges();

                return p;
            }
            catch (Exception) 
            {
                return NotFound();
            }
        }


        //PUT endpoint
        [HttpPut]
        public ActionResult<Product> Put([FromBody] Product product)
        {
            try
            {
                var prod = _context.Products.FirstOrDefault(p => p.Id == product.Id);

                prod.Name = product.Name;
                prod.ShippingFee = product.ShippingFee;
                prod.Price = product.Price;
                prod.Description = product.Description;
                prod.Stock = product.Stock;
                prod.IsUsed = product.IsUsed;
                prod.CanReturn = product.CanReturn;

                _context.SaveChanges();

                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        //DELETE endpoint
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id) 
        {
            try
            {
                var prod = _context.Products.First(p => p.Id == id);
                _context.Remove(prod);
                _context.SaveChanges();
                return Ok(prod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        //PATCH endpoint
        [HttpPatch("{id}")]
        public ActionResult<Product> Patch(int id, [FromBody] JsonPatchDocument<Product> prodPatch)
        {
            try
            {
                var prod = _context.Products.First(p => p.Id == id);
                if (prod != null)
                {
                    prodPatch.ApplyTo(prod);
                    _context.SaveChanges();
                    return Ok(prod);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }

            
        }

    }
}
