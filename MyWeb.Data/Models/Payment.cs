using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWeb.Data.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required, ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        [MaxLength(50)]
        public string? PaymentMethod { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string? PaymentStatus { get; set; }

        public DateTime? PaidAt { get; set; }
          // THÊM: Quan hệ với CashBook
        public int? CashBookId { get; set; }
        public CashBook? CashBook { get; set; }
    }
}
