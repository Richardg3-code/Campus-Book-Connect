using Campus_Book_Connect.Models;
using Campus_Book_Connect.Models.BookTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var books = await _context.Books.ToListAsync();
            return View("Market", books);
        }

    }    
}
