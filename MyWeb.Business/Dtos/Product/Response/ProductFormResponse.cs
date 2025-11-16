
using MyWeb.Business.Dtos.Brand;
using MyWeb.Business.Dtos.Category;

namespace MyWeb.Business.Dtos.Product.Response
{
    public class ProductFormResponse
    {
        public ProductDto? Product { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<BrandDto> Brands { get; set; }
    }
}