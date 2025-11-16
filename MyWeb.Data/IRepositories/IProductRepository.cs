using MyWeb.Business.Dtos.Product;
using MyWeb.Business.Dtos.Product.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Data.IRepositories
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync(int[] ids);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task<PagedResult<ProductTableResponse>> GetProductsByPagingAsync(PagingRequest request);
        Task<PagedResult<ProductTableResponse>> SearchProductsByNameAsync(string name, PagingRequest request);
        Task<ProductFormResponse> GetProductFormViewModelAsync(int Id);
        Task<bool> DeleleteSelectedProductsAsync(List<Product> productIds);
    }
}