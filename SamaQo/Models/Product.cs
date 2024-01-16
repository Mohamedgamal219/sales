using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamaQo.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<Storage> Storages { get; set; }
        public IEnumerable<SalesSheetDetail> SalesSheetDetails { get; set; }

        [NotMapped]
        public string SupplierName { get; set; }
        [NotMapped]
        public double StorageQuantity { get; set; } = 0;


    }
}
