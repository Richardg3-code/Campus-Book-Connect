using Campus_Book_Connect.Models.BookTable;
using Campus_Book_Connect.Models;

namespace Campus_Book_Connect.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Books.Any())
            {
                // DB has already been seeded
                return;
            }

            var books = new List<Book>
            {
                new Book { Title = "Intro to C#", Author = "Bro Jame", Price = 20.00M, Description = "A beginner guide", IsSold = false },
                new Book { Title = "Advanced Java", Author = "Jane mith", Price = 35.50M, Description = "Deep dive into Java", IsSold = false },
                new Book { Title = "Python Tricks", Author = "Dan Bder", Price = 25.00M, Description = "Clean and Pythonic", IsSold = false },
                new Book { Title = "Operating Systems", Author = "Galvin", Price = 45.00M, Description = "Modern OS concepts", IsSold = false },
                new Book { Title = "Networking Basics", Author = "Andrew baum", Price = 30.00M, Description = "Foundations of networking", IsSold = false },
                new Book { Title = "HTML & CSS", Author = "Jon Duck", Price = 22.00M, Description = "Frontend essentials", IsSold = false },
                new Book { Title = "Database Design", Author = "Carlos oronel", Price = 27.00M, Description = "Data modeling and SQL", IsSold = false },
                new Book { Title = "Algorithms Unlocked", Author = "Thomas men", Price = 40.00M, Description = "Understand core algorithms", IsSold = false },
              
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
