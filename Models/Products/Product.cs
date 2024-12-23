using Clgproject.Models.MainSuppliers;
using Clgproject.Models.MainTrans;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.Products
{

    public class Product
    {
        [Key]
        public int order_id { get; set; }


        public int Mainsupp_id { get; set; }

        [ForeignKey("Mainsupp_id")]
        public MainSupplier mainSupplier { get; set; }
   

        public DateTime date_of_order { get; set; }

        public string order_details { get; set; }

        public float cost { get; set; }

        public int quantity { get; set; }


    
        public string o_address { get; set; }
        
        public ICollection<ReceiveIOrder> receiveIOrders { get; set; }
        public ICollection<ReturnIOrder> returnIOrders { get; set; }

    }
}
