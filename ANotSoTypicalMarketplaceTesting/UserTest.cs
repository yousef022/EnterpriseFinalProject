using ANotSoTypicalMarketplace.Controllers;
using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Host;


//Author: Tyler Makris; Description: This class is used to test users
namespace ANotSoTypicalMarketplaceTesting
{
    public class UserTest
    {
        
        //Tests change of username
        [Fact]
        public void ChangeUserName()
        {
            User user = new User();
            user.Id = 1;
            user.UserName = "User1";
            user.FullName = "Bob Billy";
            user.UserEmail = "B@B.com";
            user.PhoneNumber = 1111111111;

            user.UserName = "User2";

            Assert.Equal("User2", user.UserName);
        }

        //Tests change of email
        [Fact]
        public void ChangeEmail()
        {
            User user = new User();
            user.Id = 1;
            user.UserName = "User1";
            user.FullName = "Bob Billy";
            user.UserEmail = "B@B.com";
            user.PhoneNumber = 1111111111;

            user.UserEmail = "T@T.com";

            Assert.Equal("T@T.com", user.UserEmail);
        }

        //Tests if null
        [Fact]
        public void CheckNullProduct()
        {

            User user = new User();
            user.Id = 1;
            user.UserName = "";
            user.FullName = "Tim Dale";
            user.UserEmail = "B@B.com";
            user.PhoneNumber = 1111111111;

            Assert.Equal("", user.UserName);
        }
    }
}
