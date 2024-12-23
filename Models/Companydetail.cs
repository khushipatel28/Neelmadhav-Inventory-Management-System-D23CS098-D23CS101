using System.ComponentModel.DataAnnotations;

namespace Clgproject.Models
{
    public class Companydetail
    {
   
        public int companyid { get; set; }
        public int d_id { get; set; }
        [Required]
        public string gstno { get; set; }

        [Required]
        public string panno { get; set; }
        [Required]
        public string faxno { get; set; }

        [Required]
        public string offwebsite { get; set; }

        [Required]
        public string socialmedialink { get; set; }

        [Required]
        public string other { get; set; }

        public int pro1_complete { get; set; }






 
    }
}
