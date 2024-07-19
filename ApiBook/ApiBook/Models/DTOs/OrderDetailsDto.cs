using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBook.Models.DTOs
{
    public class OrderDetailsDto
    {
        public int? Id { get; set; }
      
        public int IdOrder { get; set; }
        public string? Order { get; set; }

        
        public int IdBook { get; set; }
        public string? Book { get; set; }
        public string? BookAuthor { get; set; }
        public decimal? BookPrice { get; set; }



        public int Amount { get; set; }
        public decimal? Price { get; set; }
    }
}
