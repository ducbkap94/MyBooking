using System.Data.SqlTypes;
using MyWeb.Data.Models;
using MyWeb.Common.Paging;
namespace MyWeb.Service
{
    public interface IBrandService
    {
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> CreateBrandAsync(Brand brand);
        Task<Brand> UpdateBrandAsync(Brand brand);
        Task<List<Brand>> GetAllBrandsAsync(int[] ids);
        Task<bool> DeleteSelectedBrandsAsync(List<Brand> brands);
        Task<bool> DeleteBrandAsync(Brand brand);
        Task<PagedResult<Brand>> GetBrandsByPagingAsync(PagingRequest pagingRequest);
        Task<PagedResult<Brand>> GetBrandsByPagingAsync(string searchName, PagingRequest pagingRequest);
    
    }

}