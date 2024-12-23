using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.Customers
{
    public class ReturnCOrder
    {
        [Key]
        public int return_id { get; set; }
        public int Order_id { get; set; }
        [ForeignKey("Order_id")]
        public OrderC orderC { get; set; }
        public string address { get; set; }

        public DateTime date_of_order { get; set; }
        public DateTime date_of_return { get; set; }

        public string t_return { get; set; }
    }
}
