using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyWeb.Data.Models
{
    public class BookingDetail
    {
        [Key]
        public int BookingDetailId { get; set; }

        [Required, ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
