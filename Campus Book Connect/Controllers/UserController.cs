using Microsoft.AspNetCore.Mvc;

namespace Campus_Book_Connect.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
