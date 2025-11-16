using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyWebDbContext _context;
        public CategoryRepository(MyWebDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public Task<bool> DeleteCategoryAllAsync(List<Category> categories)
        {
            _context.Categories.RemoveRange(categories);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<List<Category>> GetAllCategoriesAsync(int[] ids)
        {
            return _context.Categories
                .Where(c => ids.Contains(c.CategoryId)).ToListAsync();
        }

        public async Task<PagedResult<Category>> GetCategoriesByPagingAsync(PagingRequest request)
        {
            var query = _context.Categories.AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<Category>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };

        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChangesAsync().ContinueWith(t => category);
        }
        public Task<PagedResult<Category>> GetBrandsByPagingAsync(string searchName, PagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest), "Paging request cannot be null");
            }

            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(b => b.Name != null && b.Name.Contains(searchName));
            }

            var totalRecords = query.Count();

            var items = query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((pagingRequest.Page - 1) * pagingRequest.PageSize)
                .Take(pagingRequest.PageSize)
                .ToList();

            return Task.FromResult(new PagedResult<Category>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = pagingRequest.Page,
                PageSize = pagingRequest.PageSize
            });
        }
    }
}