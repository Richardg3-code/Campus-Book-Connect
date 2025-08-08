using Campus_Book_Connect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class TransactionController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public TransactionController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> History()
    {
        var identityUserId = _userManager.GetUserId(User);
        var appUserId = await _context.AppUsers
            .Where(u => u.IdentityUserId == identityUserId)
            .Select(u => u.UserId)
            .SingleAsync();

        var transactions = await _context.Transactions
            .Include(t => t.Book)
            .Where(t => t.BuyerId == appUserId)
            .ToListAsync();

        return View(transactions);
    }
}
