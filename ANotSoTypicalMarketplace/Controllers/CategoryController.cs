//Author: Tyler Makris

using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ANotSoTypicalMarketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Database _context;

        public CategoryController(Database context)
        {
            _context= context;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => _context.Categories.ToList();


        [HttpPost]
        public Category Post([FromBody] Category category)
        {

            var c = new Category
            {
                Id = category.Id,
                Name = category.Name
            };

            _context.Categories.Add(c);
            _context.SaveChanges();

            return category;

        }

        [HttpPut]
        public Category Put([FromBody] Category category)
        {


            var cate = _context.Categories.First(c => c.Id == category.Id);
            cate.Name = category.Name;



            _context.SaveChanges();



            return category;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cate = _context.Categories.First(c => c.Id == id);
            //var productId = _context.Products.First(p => p.ProductId == id).Delete();
            _context.Remove(cate);
            _context.SaveChanges();

        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Category> catePatch)
        {
            var cate = (Category)_context.Categories.First(c => c.Id == id);
            if (cate != null)
            {
                catePatch.ApplyTo(cate);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
