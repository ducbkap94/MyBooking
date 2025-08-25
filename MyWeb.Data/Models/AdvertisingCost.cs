
using System.ComponentModel.DataAnnotations;


namespace MyWeb.Data.Models
{
    public class AdvertisingCost
    {
        [Key]
        public int AdvertisingCostId { get; set; }

        [Required]
        public DateTime DateIncurred { get; set; } = DateTime.Now;

        [Required, MaxLength(100)]
        public string? Platform { get; set; } // Ví dụ: Facebook, Google Ads, TikTok

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(255)]
        public string? CampaignName { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        // Nếu đã thanh toán bằng tiền mặt → liên kết với CashBook
        public int? CashBookId { get; set; }
        public CashBook? CashBook { get; set; }
    }
}
