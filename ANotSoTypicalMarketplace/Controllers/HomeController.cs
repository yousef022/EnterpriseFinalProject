﻿using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ANotSoTypicalMarketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly Database _context;

        public static bool userLoggedIn = false;
        static User _user = new User();

        public HomeController(Database dbContext)
        {
            _dbContext = dbContext;
        }

        Database _dbContext;
        public async Task<IActionResult> Index()
        {
            List<Product> productList = new List<Product>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    "http://localhost:5001/api/product"))
                {
                    string apiResponse = await
                                    response.Content.ReadAsStringAsync();

                    productList = JsonConvert.
                        DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(productList);
        }
        
        public IActionResult AddListingPage()
        {
            return View("AddListing");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            Product productList = new Product();
            using (HttpClient httpClient = new HttpClient())
            {

                StringContent content = new StringContent(JsonConvert.
                    SerializeObject(product), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(
                    "http://localhost:5001/api/product", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await
                                    response.Content.ReadAsStringAsync();
                        productList = JsonConvert.
                       DeserializeObject<Product>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> PutProduct(int id)
        {
            Product prod = new Product();
            ViewData["pid"] = id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/product/" + id))
                {
                    string apiRes = response.Content.ReadAsStringAsync().Result;
                    prod = JsonConvert.DeserializeObject<Product>(apiRes);
                }
            }
            return View("PutProduct", prod);

        }

        [HttpPost]
        public async Task<IActionResult> PutProduct(Product product)
        {

            string data = JsonConvert.SerializeObject(product);
            var httpClient = new HttpClient();
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.PutAsync("http://localhost:5001/api/product/", content).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("PutProduct", product);

        }

        public async Task<IActionResult> DeleteProduct(int id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5001/api/product/" + id.ToString()))
                {

                    string apiRes = await response.Content.ReadAsStringAsync();

                }

            }
            return RedirectToAction("Index");
        }
        
      
        public async Task<IActionResult> SellProduct(int id, int stock)
        {
            Product product = new Product();
            //var newStock = stock - 1;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/product" + id))
                {
                    string apiRes = response.Content.ReadAsStringAsync().Result;
                    ViewData["id"] = id;
                    ViewData["stock"] = stock;
                    ViewData["maxStock"] = stock;
                    product = JsonConvert.DeserializeObject<Product>(apiRes);

                }
            }
            return View("SaleConfirm", product);
        }

        [HttpPost]
        public async Task<IActionResult> SellProduct(int id, Product product)
        {

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/api/product/" + id),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"Stock\",\"value\": \"" + product.Stock + "\"}]",
                    Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Cart()
        {
            List<Cart> cartList = new List<Cart>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/cart"))
                {
                    string apiResponse = await
                                    response.Content.ReadAsStringAsync();

                    cartList = JsonConvert.
                        DeserializeObject<List<Cart>>(apiResponse);
                }
            }
            return View(cartList);
        }

        
        public async Task<IActionResult> AddToCart(int productId)
        {
            Cart cartList = new Cart();
            using(HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("http://localhost:5001/api/cart/" + productId, null);
               
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SellProductFromCart(int id, int stock)
        {
            Product product = new Product();
            //var newStock = stock - 1;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/product" + id))
                {
                    string apiRes = response.Content.ReadAsStringAsync().Result;
                    ViewData["id"] = id;
                    ViewData["stock"] = stock;
                    
                    product = JsonConvert.DeserializeObject<Product>(apiRes);

                }
            }
            return View("SaleConfirmCart", product);
        }

        [HttpPost]
        public async Task<IActionResult> SellProductFromCart(int id, Product product)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/api/product/" + id),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"Stock\",\"value\": \"" + product.Stock + "\"}]",
                    Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);

            }

            await DeleteFromCart(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFromCart(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5001/api/cart/" + id.ToString()))
                {
                    string apiRes = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PriceMatch(int id, string name, double price)
        {

            List<PriceMatch> priceMatchList = new List<PriceMatch>();
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    "http://localhost:5001/api/pricematch"))
                {
                    string apiResponse = await
                                    response.Content.ReadAsStringAsync();

                    priceMatchList = JsonConvert.
                        DeserializeObject<List<PriceMatch>>(apiResponse);
                }
            }

            var pmName = priceMatchList.Find(n => n.Name == name);

            var pmPrice = pmName.Price;
            

            


            //var priceMatchProductName = _context.PriceMatches.Find(name);
            if (pmPrice < price)
            {
                var newPrice = pmPrice;
                ViewData["prodid"] = id;
                ViewData["prodprice"] = newPrice;
                ViewData["prodname"] = name;
            }
            else if (pmPrice >= price)
            {
                ViewData["prodid"] = id;
                ViewData["prodprice"] = price;
                ViewData["prodname"] = name;
            }

           
           
            return View("PriceMatch");
        }

        /* List<Product> productList = new List<Product>();

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    "http://localhost:5001/api/product"))
                {
                    string apiResponse = await
                                    response.Content.ReadAsStringAsync();

                    productList = JsonConvert.
                        DeserializeObject<List<Product>>(apiResponse);
                }
            }*/
        [HttpPost]
        public async Task<IActionResult> PriceMatch(int id, Product product)
        {
           
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/api/product/" + id),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"Price\",\"value\": \"" + product.Price + "\"}]",
                    Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);

            }

            return RedirectToAction("Index");
        }

        
        public IActionResult LoginInfoCheck()
        {

            if (_dbContext.Users.Any(p => p.UserEmail == _user.UserEmail & p.Password == _user.Password))
            {
                userLoggedIn = true;
                return View("Dashboard", _user);
            }
            else
            {
                return View("Login", _user);
            }

        
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (userLoggedIn == true)
            {
                return View(_user);
            }

            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult EmailExist()
        {
            return View("EmailExist");
        }

        [HttpPost]
        public IActionResult SaveResponse(User user)
        {
            _user = user;

            if (!ModelState.IsValid)
            {
                return View("SignUp", _user);
            }

            if (ModelState.IsValid)
            {
                if (_dbContext.Users.Any(p => p.UserEmail == _user.UserEmail))
                {
                    return View("EmailExist");
                }
                //todo: save the response
               // User.AddUser(_user); //stuck here
                return View("Login", _user);
            }

            else
            {
                return View("SignUp");
            }
        }

        //stuck on adding the user
        public static void AddUser(User user)
        {
            //User.Add(user);
        }

    }
}
