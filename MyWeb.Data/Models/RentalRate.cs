using System.ComponentModel.DataAnnotations;

namespace MyWeb.Data.Models
{
    public class RentalRate
    {
        [Key]
        public int RateId { get; set; }                
        public int? ProductId { get; set; }            // FK -> Product (giá chung)
        public int? AssetId { get; set; }              // FK -> Asset (giá riêng)
        public decimal PricePerDay { get; set; }
        public decimal? PricePerHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Product? Product { get; set; }
        public Asset? Asset { get; set; }
    }

}