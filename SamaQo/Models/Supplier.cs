namespace SamaQo.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}