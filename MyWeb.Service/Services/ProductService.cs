using MyWeb.Business.Dtos.Product;
using MyWeb.Business.Dtos.Product.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;

namespace MyWeb.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public Task<bool> CreateProductAsync(ProductRequest request)
        {
            var product=new Product
            {
                Name = request.Name,
                Specification = request.Specification,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId
            };
            return _productRepo.CreateProductAsync(product);
        }

        public Task<bool> DeleleteSelectedProductsAsync(List<Product> productIds)
        {
            if(productIds == null || !productIds.Any())
            {
                throw new ArgumentException("Product IDs list cannot be null or empty", nameof(productIds));
            }
            return _productRepo.DeleleteSelectedProductsAsync(productIds);
        }

        public Task<bool> DeleteProductAsync(Product product)
        {
            
            return _productRepo.DeleteProductAsync(product);
        }

        public async Task<List<Product>> GetAllProductsAsync(int[] ids)
        {
            return await _productRepo.GetAllProductsAsync(ids);
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _productRepo.GetProductByIdAsync(id);
        }

        public Task<ProductFormResponse> GetProductFormViewModelAsync(int? id)
        {
            return _productRepo.GetProductFormViewModelAsync(id ?? 0);
        }

        public async Task<PagedResult<ProductTableResponse>> GetProductsByPagingAsync(PagingRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request), "Paging request cannot be null");
            }
            return await _productRepo.GetProductsByPagingAsync(request);
        }

        public Task<PagedResult<ProductTableResponse>> SearchProductsByNameAsync(string name, PagingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Paging request cannot be null");
            }
            return _productRepo.SearchProductsByNameAsync(name, request);
        }

        public Task<bool> UpdateProductAsync(ProductRequest request)
        {
            var product = new Product
            {
                ProductId = request.Id,
                Name = request.Name,
                Specification = request.Specification,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId
            };
            return _productRepo.CreateProductAsync(product);
        }
    }
}