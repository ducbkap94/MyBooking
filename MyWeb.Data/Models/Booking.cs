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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<BookingDetail>? BookingDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
