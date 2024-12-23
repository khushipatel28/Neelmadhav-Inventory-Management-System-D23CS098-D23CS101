using Clgproject.Models.Customers;
using System.ComponentModel.DataAnnotations;

namespace Clgproject.Models.CustTrans
{
    public class Cust_Transaction
    {
        [Key]
        public int t_id { get; set; }

        public string mode { get; set; }
        public string t_type { get; set; }
        public string t_status { get; set; }
        public ICollection<Customer> Customers { get; set; }

    }
}
