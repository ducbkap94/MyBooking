using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyWeb.Data.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DebtAmount { get; set; }


        [MaxLength(50)]
        public int? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<BookingDetail>? BookingDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
