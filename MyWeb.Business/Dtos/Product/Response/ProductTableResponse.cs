namespace MyWeb.Business.Dtos.Product
{
    public class ProductTableResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string Brand { get; set; }

        public decimal? PricePerDay { get; set; }
        public decimal? PricePerHour { get; set; }
        public string? CreatedAt { get; set; }

        public int Status { get; set; }

    }
}