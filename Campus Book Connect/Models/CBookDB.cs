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

        // Existing Books table
        public DbSet<Book> Books { get; set; }

        // Users table for login/registration
        public DbSet<User> Users { get; set; }

        // Optional: Uncomment if you later implement transactions
        // public DbSet<Transaction> Transactions { get; set; }

        // enforces unique Username & Email
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint for Username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Unique constraint for Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}

