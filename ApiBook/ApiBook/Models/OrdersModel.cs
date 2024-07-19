using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBook.Models
{
    public class OrdersModel
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public virtual UserModel User { get; set; }
    }
}
