namespace Clgproject.Models
{
    public class ProfileView
    {
        public IEnumerable<Registration> registrations { get; set; }
        public IEnumerable<Companydetail> companydetails { get; set; }
        public IEnumerable<Bankdetail> bankdetails { get; set; }
    }
}
