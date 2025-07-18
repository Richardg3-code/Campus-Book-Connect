using Microsoft.AspNetCore.Mvc;

namespace Campus_Book_Connect.Controllers
{
    public class MarketplaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
