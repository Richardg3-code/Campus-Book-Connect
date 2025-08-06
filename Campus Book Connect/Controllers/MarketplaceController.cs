using Campus_Book_Connect.Models;
using Campus_Book_Connect.Models.BookTable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Campus_Book_Connect.Controllers
{
    public class MarketplaceController : Controller
    {


        private readonly AppDbContext _context;
        

        public MarketplaceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Where(b => !b.IsSold)
                .ToListAsync();

            return View("Market", books);
        }


        [HttpGet]
        public IActionResult Sell()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sell(Book book)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Login", "User"); // Redirect to login
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
               

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Book listed successfully!";
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(string bookIds)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (string.IsNullOrEmpty(bookIds))
            {
                TempData["SuccessMessage"] = "No books selected for purchase.";
                return RedirectToAction("Index");
            }

            var ids = bookIds.Split(',').Select(int.Parse).ToList();
            var books = await _context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();

            foreach (var book in books)
            {
                // Skip already sold books, just in case
                if (book.IsSold) continue;

                book.IsSold = true;

                var transaction = new Transaction
                {
                    BookId = book.Id,
                    BuyerId = userId.Value,
                    PriceAtPurchase = book.Price,
                    TransactionDate = DateTime.Now
                };

                _context.Transactions.Add(transaction);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you! Your books have been purchased.";
            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout([FromForm] List<int> bookIds)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var books = await _context.Books
                .Where(b => bookIds.Contains(b.Id) && !b.IsSold)
                .ToListAsync();

            foreach (var book in books)
            {
                var transaction = new Transaction
                {
                    BookId = book.Id,
                    BuyerId = userId.Value,
                    PriceAtPurchase = book.Price,
                    TransactionDate = DateTime.Now
                };

                _context.Transactions.Add(transaction);
                book.IsSold = true;
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Checkout complete. Your purchases are in your transaction history.";
            return RedirectToAction("Index");
        }











    }
}
