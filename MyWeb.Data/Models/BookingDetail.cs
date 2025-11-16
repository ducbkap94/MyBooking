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

        [Required, ForeignKey("Asset")]
        public int AssetId { get; set; }
        public Asset? Asset { get; set; }

        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Days { get; set; }
        public int Status { get; set; }
        public string? RateType { get; set; }

    }
}
