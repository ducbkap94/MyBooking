
using System.ComponentModel.DataAnnotations;


namespace MyWeb.Data.Models
{
    public class CashBook
    {
        [Key]
        public int CashBookId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        // Thu = true, Chi = false
        public bool IsIncome { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        // Liên kết đến Payment (nếu giao dịch này là thu từ khách hàng)
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        // Liên kết đến Maintenance (nếu giao dịch này là chi phí bảo trì)
        public int? MaintenanceId { get; set; }
        public Maintenance? Maintenance { get; set; }

        // Liên kết đến AdvertisingCost (nếu giao dịch này là chi phí quảng cáo)
        public int? AdvertisingCostId { get; set; }
        public AdvertisingCost? AdvertisingCost { get; set; }

        [MaxLength(50)]
        public string? CreatedBy { get; set; }
        
    }
}
