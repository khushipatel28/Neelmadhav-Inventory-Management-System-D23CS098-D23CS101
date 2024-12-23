namespace Clgproject.Models.MainSuppliers
{
    public class MainSupplierHistory
    {
        public int Id { get; set; }
        public int MainSupplierId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionId { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
