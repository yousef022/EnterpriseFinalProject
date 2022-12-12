using ANotSoTypicalMarketplace.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ANotSoTypicalMarketplace.Models;

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

        [HttpGet]
        public IEnumerable<Product> Get() => _context.Products.ToList();

        //Adding a product
        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            var p = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ShippingFee= product.ShippingFee,
                IsUsed= product.IsUsed,
                CanReturn = product.CanReturn,
                CategoryId = product.CategoryId
            };

            _context.Products.Add(p);
            _context.SaveChanges();

            return product;
        }

        //Updating a product
        [HttpPut]
        public Product Put([FromBody] Product product)
        {

            var prod = _context.Products.First(p => p.Id == product.Id);

            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Price = product.Price;
            prod.ShippingFee= product.ShippingFee;
            prod.IsUsed = product.IsUsed;
            prod.CanReturn= product.CanReturn;
            prod.CategoryId = product.CategoryId;

            _context.SaveChanges();

            return product;
        }



    }
}
