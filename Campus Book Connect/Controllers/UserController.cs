using Microsoft.AspNetCore.Mvc;
using Campus_Book_Connect.Models;
using System.Linq;
using BCrypt.Net; // Make sure BCrypt.Net-Next is installed

namespace Campus_Book_Connect.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;  // ✅ Use AppDbContext instead

        public UserController(AppDbContext context) // ✅ Inject AppDbContext
        {
            _context = context;
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // ✅ Save session
                HttpContext.Session.SetString("User", user.Username);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // GET: /User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        public IActionResult Register(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // GET: /User/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
