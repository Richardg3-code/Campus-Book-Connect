using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// this is the book atributes 
namespace Campus_Book_Connect.Models.BookTable
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }
       

    }
}
