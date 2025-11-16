using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;

namespace MyWeb.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            return _categoryRepository.GetCategoryByIdAsync(id);
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return _categoryRepository.GetAllCategoriesAsync();
        }

        public Task<Category> CreateCategoryAsync(Category category)
        {
            return _categoryRepository.CreateCategoryAsync(category);
        }

        public Task<bool> DeleteCategoryAllAsync(List<Category> categories)
        {
            return _categoryRepository.DeleteCategoryAllAsync(categories);
        }

        public Task<bool> DeleteCategoryAsync(Category category)
        {
            return _categoryRepository.DeleteCategoryAsync(category);
        }

        public Task<PagedResult<Category>> GetCategoriesByPagingAsync(PagingRequest pagingRequest)
        {
            return _categoryRepository.GetCategoriesByPagingAsync(pagingRequest);
        }

        public Task<List<Category>> GetAllCategoriesAsync(int[] ids)
        {
            return _categoryRepository.GetAllCategoriesAsync(ids);
        }

        public Task<Category> UpdateCategoryAsync(Category category)
        {
            return _categoryRepository.UpdateCategoryAsync(category);
        }
        public Task<PagedResult<Category>> GetBrandsByPagingAsync(string searchName, PagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest), "Paging request cannot be null");
            }

            return _categoryRepository.GetBrandsByPagingAsync(searchName, pagingRequest);
        }
    }
}