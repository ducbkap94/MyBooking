using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyWeb.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required, MaxLength(150)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ProductImage>? ProductImages { get; set; }
        public Inventory? Inventory { get; set; }
        public ICollection<Maintenance>? Maintenances { get; set; }


        // Quan hệ 1-n: Brand - Product
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        // Quan hệ 1-n: Product - CashBook
        public ICollection<CashBook>? CashBooks { get; set; }

        // Quan hệ 1-n: Product - BookingDetail
        public ICollection<BookingDetail>? BookingDetails { get; set; }

        // Quan hệ 1-n: Product - Payment
        public ICollection<Payment>? Payments { get; set; }

    }
}
