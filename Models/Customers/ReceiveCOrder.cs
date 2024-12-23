using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.Customers
{
    public class ReceiveCOrder
    {
        [Key]
        public int receive_id { get; set; }


        public int Order_id { get; set; }
        [ForeignKey("Order_id")]
        public OrderC orderC { get; set; }

        public DateTime receive_datetime { get; set; }
    }
}
