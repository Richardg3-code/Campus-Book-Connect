using Microsoft.AspNetCore.Mvc;
using Campus_Book_Connect.Models;
using System.Linq;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity; // Make sure BCrypt.Net-Next is installed
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Campus_Book_Connect.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;  //  Use AppDbContext instead
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(AppDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) //  Inject AppDbContext
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: false);
            if (user.Succeeded)
            {
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
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (_context.AppUsers.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var idUser = new IdentityUser { UserName = username, Email = email};
            var newUsr = await _userManager.CreateAsync(idUser, password);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                IdentityUserId = idUser.Id,
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            _context.AppUsers.Add(newUser);
            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(idUser, isPersistent: false);

            return RedirectToAction("Login");
        }

        // GET: /User/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.AppUsers.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
}
