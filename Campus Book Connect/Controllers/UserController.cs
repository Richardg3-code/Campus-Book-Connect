using System.Linq;
using System.Threading.Tasks;
using Campus_Book_Connect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Book_Connect.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // -------- Register --------
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            // Basic duplicate username check against your custom table
            if (await _context.AppUsers.AnyAsync(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var idUser = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(idUser, password);

            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return View();
            }

            var appUser = new User
            {
                IdentityUserId = idUser.Id,
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.AppUsers.Add(appUser);
            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(idUser, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        // -------- Login / Logout --------
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(
                userName: username,
                password: password,
                isPersistent: true,
                lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // -------- Profile --------
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var identityUserId = _userManager.GetUserId(User); // string id from AspNetUsers
            if (identityUserId == null)
                return RedirectToAction(nameof(Login));

            var appUser = await _context.AppUsers
                                        .SingleOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (appUser == null)
                return NotFound();

            return View(appUser); // Views/User/Profile.cshtml expects @model Campus_Book_Connect.Models.User
        }

        // Optional: if you ever wire AccessDeniedPath to this
        [HttpGet]
        public IActionResult AccessDenied() => RedirectToAction(nameof(Login));
    }
}
