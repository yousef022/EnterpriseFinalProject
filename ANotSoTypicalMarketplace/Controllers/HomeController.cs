using ANotSoTypicalMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
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
        
       public async Task<IActionResult> SellProduct(int id)
        {
            Product product = new Product();
            var prod = _context.Products.Find(product.Id);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/product/" + id))
                {
                    string apiRes = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(apiRes);
                }
            }
            prod.Stock -= prod.Stock;
            return View("SaleConfirm");
        }

        
        public IActionResult LoginFormCheck()
        {
            if (User.Any(p => p.UserEmail == _user.UserEmail & p.Password == _user.Password))
            {
                return View("LoginForm");
            }
            else
            {
                userLoggedIn = true;
                return View("Dashboard", _user);
            }


        }



    }
}
