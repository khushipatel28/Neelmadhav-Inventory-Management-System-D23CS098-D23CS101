using Clgproject.Models.Customers;
using System.ComponentModel.DataAnnotations;

namespace Clgproject.Models.Suppliers
{
    public class Supplier
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string address { get; set; }
         
        public string contact { get; set; }

        public ICollection<OrderC> orderCs { get; set; }

       
    }
}
