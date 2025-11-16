using MyWeb.Common.Paging;
using MyWeb.Data.Models;
namespace MyWeb.Service.Interfaces
{

    public interface ICategoryService
    {
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(Category category);
        Task<bool> DeleteCategoryAllAsync(List<Category> categories);
        Task<PagedResult<Category>> GetCategoriesByPagingAsync(PagingRequest pagingRequest);
        Task<List<Category>> GetAllCategoriesAsync(int[] ids);
        Task<PagedResult<Category>> GetBrandsByPagingAsync(string searchName, PagingRequest pagingRequest);
    }
}