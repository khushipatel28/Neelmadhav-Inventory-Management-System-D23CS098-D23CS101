using System.ComponentModel.DataAnnotations;
using System;
namespace Clgproject.Models
{
    public class Registration
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        [StringLength(40, ErrorMessage = "must between 3 to 16 characters", MinimumLength = 3)]

        public string company_name { get; set; }

        [Required]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "A valid email address consists of an email prefix and an email domain, both in acceptable formats.")]

        public string email { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        [StringLength(10, ErrorMessage = "NUMBER must be Of 10 Number.", MinimumLength = 10)]
        public string mobile_no { get; set; }

        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8}$", ErrorMessage = "Password must be contain upper letter,lower letter,numbers,and The number of character must be 8")]

        public string pass_word { get; set; }

        [Required]
        [Compare("pass_word")]

        public string confirm_pass { get; set; }

        [Required]
        public string companyaddress { get; set; }

        [Required]
        public string country { get; set; }

        [Required]
        public string comapany_state { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string pincode { get; set; }


        public int is_profile_complete { get; set; }


    }
}
