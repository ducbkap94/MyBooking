using System.ComponentModel.DataAnnotations;

namespace MyWeb.Data.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required, MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(50)]
        public string? DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Status { get; set; }
    }
}
