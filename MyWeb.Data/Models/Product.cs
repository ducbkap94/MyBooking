using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyWeb.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required, MaxLength(150)]
        public string? Name { get; set; }

        public string? Specification { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // Quan hệ 1-n: Brand - Product
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<RentalRate> RentalRates { get; set; } = new List<RentalRate>();

        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
    


    }
}
