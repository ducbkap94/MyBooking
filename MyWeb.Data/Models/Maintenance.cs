using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWeb.Data.Models
{
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public DateTime MaintenanceDate { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string? MaintenanceType { get; set; } // Ví dụ: Sửa chữa, Vệ sinh, Nâng cấp

        [MaxLength(255)]
        public string? Description { get; set; }

        public decimal Cost { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; } // Đang bảo trì, Hoàn thành
    }
}
