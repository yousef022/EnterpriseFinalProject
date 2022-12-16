//Author: Yousef Osman

using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ANotSoTypicalMarketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceMatchController : ControllerBase
    {
        private readonly Database _context;

        public PriceMatchController(Database context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<PriceMatch> Get() => _context.PriceMatches.ToList();

        [HttpPost]
        public PriceMatch Post([FromBody] PriceMatch priceMatch)
        {
            var p = new PriceMatch
            {
                Id = priceMatch.Id,
                Origin = priceMatch.Origin,
                Name = priceMatch.Name,
                Price = priceMatch.Price,
                CategoryId = priceMatch.CategoryId
            };

            _context.PriceMatches.Add(p);
            _context.SaveChanges();

            return priceMatch;
        }

        [HttpPut]
        public PriceMatch Put([FromBody] PriceMatch priceMatch)
        {
            var pm = _context.PriceMatches.First(p => p.Id == priceMatch.Id);

            pm.Origin = priceMatch.Origin;
            pm.Name = priceMatch.Name;
            pm.Price = priceMatch.Price;
            pm.CategoryId = priceMatch.CategoryId;

            _context.SaveChanges();

            return priceMatch;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) 
        {
            var pm = (PriceMatch)_context.PriceMatches.First(p => p.Id == id);
            _context.Remove(pm);
            _context.SaveChanges();
        }
    }
}
