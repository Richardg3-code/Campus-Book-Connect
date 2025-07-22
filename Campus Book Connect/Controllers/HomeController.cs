using System.Diagnostics;
using Campus_Book_Connect.Models;
using Microsoft.AspNetCore.Mvc;

namespace Campus_Book_Connect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        
       
    }
}
