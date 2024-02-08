using ANotSoTypicalMarketplace.Models;
using ANotSoTypicalMarketplace.ViewModels;
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
        private static bool _isLoggedIn;
        private bool IsLoggedIn { get => _isLoggedIn; set => _isLoggedIn = value; }
        //static User _user = new User();

        public HomeController(Database context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            ViewData["ShowCart"] = true; // Show cart on the home page

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

        
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult GoToLogin()
        {
            return View("Login");
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
                    ViewData["maxStockCart"] = stock;

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


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                // If model state is not valid, return the same view to show validation errors
                return View("Login",loginViewModel);
            }

            List<User> userList = new List<User>();
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/user"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }

            var user = userList.FirstOrDefault(u => u.UserEmail == loginViewModel.Email);

            if (user != null && user.Password == loginViewModel.Password) // You should check the hashed password here
            {
                // User is found and password matches
                IsLoggedIn = true; // Make sure this is actually managed in your session or authentication mechanism
                ViewData["UserName"] = user.UserName;
                return RedirectToAction("Index");
            }
            else
            {
                // User is not found or password does not match
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginViewModel);
            }
        }
 
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                // The model state is invalid, return to the form with the current user model to display validation errors
                return View("SignUp", user);
            }

            User userList= new User();
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.
                   SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(
                    "http://localhost:5001/api/user", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await
                                    response.Content.ReadAsStringAsync();
                        userList = JsonConvert.
                       DeserializeObject<User>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        

       







    }
}
