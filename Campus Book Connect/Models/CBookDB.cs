using Campus_Book_Connect.Models.BookTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Campus_Book_Connect.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Existing Books table
        public DbSet<Book> Books { get; set; }

        // Users table for login/registration
        public DbSet<User> AppUsers { get; set; }

      
        // enforces unique Username & Email
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint for Username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // for IdentityUser one to one relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.IdentityUser)
                .WithOne()
                .HasForeignKey<User>(u => u.IdentityUserId);

            // Unique constraint for Email
            modelBuilder.Entity<Book>()
               .Property(b => b.Price)
               .HasColumnType("decimal(18,2)");
        }

        public DbSet<Transaction> Transactions { get; set; }

    }
}

