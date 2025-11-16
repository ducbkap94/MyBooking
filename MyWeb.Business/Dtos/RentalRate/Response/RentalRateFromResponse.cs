using MyWeb.Business.Dtos.Product;

namespace MyWeb.Business.Dtos.RentalRate
{
    public class RentalRateFromResponse
    {
       public List<ProductDto> Products { get; set; }
        public RentalRateDto? RentalRate { get; set; }
    }
}