using System.ComponentModel.DataAnnotations.Schema;

namespace SamaQo.Models
{
    public class Storage
    {
        public  int  Id { get; set; }
        public double Quantity { get; set; }
        public DateTime DateOfStorage { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Products { get; set; }


        [NotMapped]
        public string ProdName { get; set; }



    }
}
