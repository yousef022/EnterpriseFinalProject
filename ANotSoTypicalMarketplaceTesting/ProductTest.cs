using ANotSoTypicalMarketplace.Controllers;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Host;


//Author: Tyler Makris; Description: This class is used to test products
namespace ANotSoTypicalMarketplaceTesting
{
    public class ProductTest
    {
        private readonly Database _context;

        //Tests change of stock
        [Fact]
        public void ChangeStock()
        {
            Product product = new Product();
            product.Id = 1;
            product.Name = "Xbox";
            product.Description = "Game system";
            product.Stock = 10;
            product.Price = 499;
            product.ShippingFee = 15;
            product.IsUsed = false;
            product.CanReturn = false;

            product.Stock = 9;

            Assert.Equal(9, product.Stock);
        }

        //Tests change of price
        [Fact]
        public void ChangePrice()
        {
            Product product = new Product();
            product.Id = 1;
            product.Name = "Xbox";
            product.Description = "Game system";
            product.Stock = 10;
            product.Price = 499;
            product.ShippingFee = 15;
            product.IsUsed = false;
            product.CanReturn = false;

            product.Price = 399;

            Assert.Equal(399, product.Price);
        }

        //Tests if null
        [Fact]
        public void CheckNullProduct()
        {

            Product product = new Product();
            product.Id = 1;
            product.Name = "";
            product.Description = "Game system";
            product.Stock = 10;
            product.Price = 499;
            product.ShippingFee = 15;
            product.IsUsed = false;
            product.CanReturn = false;

            product.Price = 399;


            Product prod = new Product();
            prod.Name = "";
            product.Description = "Game system";
            product.Stock = 10;
            product.Price = 499;
            product.ShippingFee = 15;
            product.IsUsed = false;
            product.CanReturn = false;

            product.Price = 399;

            Assert.Equal(prod.Name, product.Name);
        }
    }
}
