using Campus_Book_Connect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Book_Connect.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult History()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "User");

            var transactions = _context.Transactions
                .Include(t => t.Book)
                .Where(t => t.BuyerId == userId)
                .ToList();

            return View(transactions);
        }
    }
}
