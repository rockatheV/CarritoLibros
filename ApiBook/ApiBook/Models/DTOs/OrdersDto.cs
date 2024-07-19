using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBook.Models.DTOs
{
    public class OrdersDto
    {
        public int? Id { get; set; }
        public DateTime FechaPedido { get; set; }

        public int IdUser { get; set; }

        public int? Quantity {  get; set; }

        public double? PriceTotal { get; set;}
    }
}
