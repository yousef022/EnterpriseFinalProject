using Microsoft.AspNetCore.Mvc;

namespace ANotSoTypicalMarketplace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
