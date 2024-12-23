using Clgproject.Models.Products;
using Clgproject.Models.MainTrans;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Clgproject.Models.MainSuppliers
{
    public class MainSupplier
    {
        [Key]
      public int  mainsupp_id { get; set; }
        public string mainsupp_name { get; set; }
        public string mainsupp_contact { get; set; }
        public string mainsupp_address { get; set; }

        
        public int MainTransactionId { get; set; }

        [ForeignKey("MainTransactionId")]
        public MainTransaction mainTransactions { get; set; }


        public string  account_no { get; set; }

        public DateTime created_at { get; set; }
         
        public ICollection<Product> products { get; set; }
    }
}
