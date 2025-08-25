
using System.ComponentModel.DataAnnotations;

namespace MyWeb.Data.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên thương hiệu không được để trống"), MaxLength(100)]
        
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Quan hệ: 1 Brand có nhiều Product
        public ICollection<Product>? Products { get; set; }
    }
}
