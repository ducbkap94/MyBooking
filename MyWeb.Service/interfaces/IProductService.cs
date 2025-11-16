using MyWeb.Business.Dtos.Product;
using MyWeb.Business.Dtos.Product.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Service.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync(int[] ids);
        Task<Product> GetProductByIdAsync(int id);
        Task<PagedResult<ProductTableResponse>> GetProductsByPagingAsync(PagingRequest request);
        Task<bool> CreateProductAsync(ProductRequest request);
        Task<bool> UpdateProductAsync(ProductRequest request);
        Task<bool> DeleteProductAsync(Product product);
        Task<PagedResult<ProductTableResponse>> SearchProductsByNameAsync(string name, PagingRequest request);
        Task<ProductFormResponse> GetProductFormViewModelAsync(int? id);

        Task<bool> DeleleteSelectedProductsAsync(List<Product> productIds);
    }
}