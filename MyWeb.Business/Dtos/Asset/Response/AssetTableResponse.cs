namespace MyWeb.Business.Dtos.Asset.Response
{
    public class AssetTableResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Supplier { get; set; } = null!;
        public decimal PurchasePrice { get; set; }
        public string? PurchaseDate { get; set; }
        public int Status { get; set; } = 1;
        public int WarrantyPeriod { get; set; }
        public string? CreatedAt { get; set; }
    }
}