using Clgproject.Models.MainSuppliers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clgproject.Models.MainTrans
{
    public class MainTransaction
    {
        [Key]
        public int t_id { get; set; }

        public string mode { get; set; }
        public string t_type { get; set; }
        public string t_status { get; set; }
        public ICollection<MainSupplier> MainSuppliers { get; set; }

    }
}
