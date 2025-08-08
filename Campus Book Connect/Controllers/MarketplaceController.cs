using Campus_Book_Connect.Models;
using Campus_Book_Connect.Models.BookTable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class MarketplaceController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MarketplaceController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private async Task<int> GetCurrentAppUserIdAsync()
    {
        var identityUserId = _userManager.GetUserId(User);
        var appUser = await _context.AppUsers
            .SingleOrDefaultAsync(u => u.IdentityUserId == identityUserId);

        if (appUser == null)
            throw new InvalidOperationException("App user not found.");
        return appUser.UserId;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.Where(b => !b.IsSold).ToListAsync();
        return View("Market", books);
    }

    [HttpGet]
    public IActionResult Sell() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sell(Book book)
    {
        if (!ModelState.IsValid) return View(book);

        book.IsSold = false;
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Book posted successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Buy(string bookIds)
    {
        if (string.IsNullOrWhiteSpace(bookIds))
        {
            TempData["SuccessMessage"] = "No books selected for purchase.";
            return RedirectToAction("Index");
        }

        var appUserId = await GetCurrentAppUserIdAsync();

        var ids = bookIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var books = await _context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();

        foreach (var book in books.Where(b => !b.IsSold))
        {
            book.IsSold = true;
            _context.Transactions.Add(new Transaction
            {
                BookId = book.Id,
                BuyerId = appUserId,
                PriceAtPurchase = book.Price,
                TransactionDate = DateTime.Now
            });
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Thank you! Your books have been purchased.";
        return RedirectToAction("Index");
    }
}

