using ANotSoTypicalMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Tyler Makris; Description: This class is used to test pricematch
namespace ANotSoTypicalMarketplaceTesting
{
    public class PriceMatchTest
    {

        //Tests change of origin
        [Fact]
        public void ChangeOrigin()
        {
            PriceMatch pricematch = new PriceMatch();
            pricematch.Id = 1;
            pricematch.Origin = "Walmart";
            pricematch.Name = "PS5";
            pricematch.Price = 599;
            pricematch.CategoryId = 3;

            pricematch.Origin = "Gamestop";

            Assert.Equal("Gamestop", pricematch.Origin);
        }

        //Tests change of name
        public void ChangeName()
        {
            PriceMatch pricematch = new PriceMatch();
            pricematch.Id = 1;
            pricematch.Origin = "Walmart";
            pricematch.Name = "Ps5";
            pricematch.Price = 599;
            pricematch.CategoryId = 3;

            pricematch.Name = "Xbox";

            Assert.Equal("Xbox", pricematch.Name);
        }

        //Tests if null
        [Fact]
        public void CheckNullPriceMatch()
        {
            PriceMatch pricematch = new PriceMatch();
            pricematch.Id = 1;
            pricematch.Origin = "";
            pricematch.Name = "Ps5";
            pricematch.Price = 599;
            pricematch.CategoryId = 3;

            pricematch.Name = "Xbox";

            Assert.Equal("", pricematch.Origin);


        }
    }
}
