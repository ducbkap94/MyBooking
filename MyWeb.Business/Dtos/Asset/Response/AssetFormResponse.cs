using MyWeb.Business.Dtos.Product;

namespace MyWeb.Business.Dtos.Asset.Response
{
    public class AssetFormResponse
    {
        public AssetDto? Asset { get; set; } 
        public List<ProductDto> Products { get; set; } 
        public List<UserDto> Suppliers { get; set; }
    }
}