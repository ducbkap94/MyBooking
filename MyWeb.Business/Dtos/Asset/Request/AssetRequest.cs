namespace MyWeb.Business.Dtos.Asset
{

    public class AssetRequest
    {
        public int AssetId { get; set; }
        public int ProductId { get; set; }             // FK -> Product
        public string SerialNumber { get; set; } = null!;
        public int Status { get; set; } = 1; // Available / Rented / Maintenance
        public int? OwnerId { get; set; }              // FK -> Owner (nếu có)
        public int? SupplierId { get; set; }  // FK -> Supplier (nếu có)
        public decimal PurchasePrice{ get; set; }         
        public DateTime? PurchaseDate { get; set; }
        public int WarrantyPeriod{ get; set; }      //Số tháng bảo hành
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
    }

}