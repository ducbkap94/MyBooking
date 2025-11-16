namespace MyWeb.Business.Dtos.Asset.Response
{
    public class AssetSelectResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Supplier { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }


    }
}