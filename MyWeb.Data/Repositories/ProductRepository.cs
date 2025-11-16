using Microsoft.EntityFrameworkCore;
using MyWeb.Business.Dtos.Brand;
using MyWeb.Business.Dtos.Category;
using MyWeb.Business.Dtos.Product;
using MyWeb.Business.Dtos.Product.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyWebDbContext _context;
        public ProductRepository(MyWebDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> DeleleteSelectedProductsAsync(List<Product> products)
        {
            if (products == null || !products.Any())
            {
                throw new ArgumentException("Product list cannot be null or empty", nameof(products));
            }
            _context.Products.RemoveRange(products);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<List<Product>> GetAllProductsAsync(int[] ids)
        {
            return _context.Products
                .Where(p => ids.Contains(p.ProductId)).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<ProductFormResponse> GetProductFormViewModelAsync(int id)
        {
            var product = _context.Products
                .Where(p => p.ProductId == id)
                .AsEnumerable();
            var lstBrand=new List<CategoryDto>(
                _context.Categories.AsEnumerable().Select(c => new CategoryDto
                {
                    Id = c.CategoryId,
                    Name = c.Name
                }).ToList()
            
            );
            var lstbrand = new List<BrandDto>(
                _context.Brands.AsEnumerable().Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList()
            );
            return await Task.FromResult(new ProductFormResponse
            {
                Product = product.Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Specification = p.Specification,
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId,

                }).FirstOrDefault(),
                Categories = lstBrand,
                Brands = lstbrand
            });
        }

        public async Task<PagedResult<ProductTableResponse>> GetProductsByPagingAsync(PagingRequest request)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                        join b in _context.Brands on p.BrandId equals b.Id
                        select new ProductTableResponse
                        {
                            Id = p.ProductId,
                            Name = p.Name,
                            Specification = p.Specification,
                            Category = c.Name,
                            Brand = b.Name,
                            CreatedAt = p.CreatedAt.ToString("dd/MM/yyyy"),

                        };
            var result = await query.AsQueryable().ToListAsync();

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<ProductTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResult<ProductTableResponse>> SearchProductsByNameAsync(string name, PagingRequest request)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                        join b in _context.Brands on p.BrandId equals b.Id
                        select new ProductTableResponse
                        {
                            Id = p.ProductId,
                            Name = p.Name,
                            Specification = p.Specification,
                            Category = c.Name,
                            Brand = b.Name,
                            CreatedAt = p.CreatedAt.ToString("dd/MM/yyyy"),

                        };
            var result = await query.AsQueryable().ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(name));
            }
            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<ProductTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            return Task.FromResult(_context.Products.Update(product).Entity);
        }
    }
}