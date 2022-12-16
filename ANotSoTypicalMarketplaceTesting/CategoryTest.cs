using ANotSoTypicalMarketplace.Controllers;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Host;


//Author: Tyler Makris; Description: This class is used to test categories
namespace ANotSoTypicalMarketplaceTesting
{
    public class CategoryTest
    {
        
        //Tests change of name
        [Fact]
        public void ChangeName()
        {
            Category category = new Category();
            category.Id = 3;
            category.Name = "Electronics";
            

            category.Name = "Clothing";

            Assert.Equal("Clothing", category.Name);
        }

        //Tests if null
        [Fact]
        public void CheckNullCategory()
        {

            Category category = new Category();
            category.Id = 3;
            category.Name = "";

            Assert.Equal("", category.Name);

            
        }
    }
}
