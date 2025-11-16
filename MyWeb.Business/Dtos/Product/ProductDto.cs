namespace MyWeb.Business.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        
    }
}