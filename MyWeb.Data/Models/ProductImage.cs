using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWeb.Data.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public bool IsMain { get; set; }
    }
}
