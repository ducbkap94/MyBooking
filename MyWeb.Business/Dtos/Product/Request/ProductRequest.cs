namespace MyWeb.Business.Dtos.Product
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int BrandId { get; set; }
        
    }
}