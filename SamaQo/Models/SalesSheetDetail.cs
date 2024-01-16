using System.ComponentModel.DataAnnotations.Schema;

namespace SamaQo.Models
{
    public class SalesSheetDetail
    {
        public int Id { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal Quantity { get; set; } 
        public decimal UintPrice { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }

        
        [NotMapped]
        public string ProName { get; set; }
        [NotMapped]
        public decimal TotalBile { get; set; }
    }

}
