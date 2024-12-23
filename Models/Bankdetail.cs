using System.ComponentModel.DataAnnotations;

namespace Clgproject.Models
{
    public class Bankdetail
    {
        public int bank_id { get; set; }
      
        public int companyid { get; set; }

        [Required]
        public string bankname { get; set; }
        [Required]
        public string customername { get; set; }

        [Required]
        public int accountno { get; set; }

        [Required]
        public string ifsccode { get; set; }

        [Required]
        public string branchname { get; set; }

        [Required]
        public string branchaddress { get; set; }
  
        public int pro2_complete { get; set; }
    }

}
