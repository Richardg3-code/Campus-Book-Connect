using Campus_Book_Connect.Models.BookTable;
using Microsoft.EntityFrameworkCore;

namespace Campus_Book_Connect.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // we can add this to the db just store the users information 
       // public DbSet<User> Users { get; set; }


        public DbSet<Book> Books { get; set; }




        // we can store transcations but idk we can delete if its too difficult
        // public DbSet<Transaction> Transactions { get; set; }
    }
}