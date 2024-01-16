namespace SamaQo.Models
{
    public class DsProductSupplier
    {
        public IEnumerable<Product> GetProducts { get; set; }
        public IEnumerable<Supplier> GetSuppliers  { get; set; }
    }
}
