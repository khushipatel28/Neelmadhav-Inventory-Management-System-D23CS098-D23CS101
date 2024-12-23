using Clgproject.Models.Suppliers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.Customers
{
    public class OrderC
    {
        [Key]
        public int order_id { get; set; }

        public int Supplier_id { get; set; }
        [ForeignKey("Supplier_id")]
        public Supplier supplier { get; set; }

        public DateTime date_of_order { get; set; }
         public string order_details { get; set; }
     
        public float cost { get; set; }

        public int quantity { get; set; }

        public string o_address { get; set; }
        
        public ICollection<ReturnCOrder> returnCOrders { get; set; }
        public ICollection<ReceiveCOrder> receiveCOrders { get; set; }


    }
}
