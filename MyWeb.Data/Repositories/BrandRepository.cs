
using MyWeb.Data.Models;
using MyWeb.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using MyWeb.Common.Paging;

namespace MyWeb.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MyWebDbContext _context;

        public BrandRepository(MyWebDbContext context)
        {
            _context = context;
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public Task<Brand> UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChangesAsync();
            return Task.FromResult(brand);
        }

        public Task<bool> DeleteBrandAsync(Brand brand)
        {
            _context.Brands.Remove(brand);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<List<Brand>> GetAllBrandsAsync(int[] ids)
        {
            return _context.Brands
                .Where(b => ids.Contains(b.Id))
                .ToListAsync();
        }

        public Task<bool> DeleteBrandAllAsync(List<Brand> brands)
        {
            _context.Brands.RemoveRange(brands);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<PagedResult<Brand>> GetBrandsPagingAsync(PagingRequest request)
        {
            var query = _context.Brands.AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Brand>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };

        }

        public Task<PagedResult<Brand>> GetBrandsPagingAsync(string searchName, PagingRequest request)
        {
            var query = _context.Brands.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(b => b.Name.Contains(searchName));
            }

            var totalRecords = query.Count();

            var items = query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var result = new PagedResult<Brand>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return Task.FromResult(result);
        }
    }
}
