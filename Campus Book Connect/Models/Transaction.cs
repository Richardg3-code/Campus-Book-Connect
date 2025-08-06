using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Campus_Book_Connect.Models.BookTable;

namespace Campus_Book_Connect.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; } = null!;

        public int BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public User Buyer { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }
    }
}
