using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBook.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public virtual OrdersModel Order {  get; set; }

        [ForeignKey("Book")]
        public int IdBook { get; set; }
        public virtual BooksModel Book { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
