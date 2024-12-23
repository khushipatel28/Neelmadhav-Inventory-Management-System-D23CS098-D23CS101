using Clgproject.Models.CustTrans;
using Clgproject.Models.MainTrans;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.Customers
{
    public class Customer
    {
        [Key]
        public int customer_id { get; set; }
        
        public int Cust_Transaction_id { get; set; }

        [ForeignKey("Cust_Transaction_id")]
        public Cust_Transaction cust_Transaction { get; set; }


        public string name { get; set; }
        public string cust_address { get; set; }
      
  
        public string account_no { get; set; }
    }
}
