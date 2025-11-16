using MyWeb.Data.Models;
using MyWeb.Common.Paging;
namespace MyWeb.Data.IRepositories
{
    public interface IBrandRepository
    {
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<List<Brand>> GetAllBrandsAsync(int[] ids);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> CreateBrandAsync(Brand brand);
        Task<Brand> UpdateBrandAsync(Brand brand);
        Task<bool> DeleteBrandAllAsync(List<Brand> brands);
        Task<bool> DeleteBrandAsync(Brand brand);
        Task<PagedResult<Brand>> GetBrandsPagingAsync(PagingRequest request);
        Task<PagedResult<Brand>> GetBrandsPagingAsync(string searchName, PagingRequest request);

    }
}