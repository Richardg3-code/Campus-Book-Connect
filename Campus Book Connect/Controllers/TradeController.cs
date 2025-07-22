using Microsoft.AspNetCore.Mvc;

namespace Campus_Book_Connect.Controllers
{
    public class TradeController : Controller
    {
        public IActionResult Index()
        {
            return View("Trade");
        }
    }
}
