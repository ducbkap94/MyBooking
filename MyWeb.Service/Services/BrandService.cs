using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;
namespace MyWeb.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepo;

        public BrandService(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _brandRepo.GetBrandByIdAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepo.GetAllBrandsAsync();
        }

        public Task<Brand> CreateBrandAsync(Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand), "Brand cannot be null");
            }

            return _brandRepo.CreateBrandAsync(brand);
        }

        public Task<Brand> UpdateBrandAsync(Brand brand)
        {
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand), "Brand cannot be null");
            }

            return _brandRepo.UpdateBrandAsync(brand);
        }


        public Task<List<Brand>> GetAllBrandsAsync(int[] ids)
        {
            return _brandRepo.GetAllBrandsAsync(ids);
        }

        public Task<bool> DeleteSelectedBrandsAsync(List<Brand> brands)
        {
            return _brandRepo.DeleteBrandAllAsync(brands);
        }

        public Task<bool> DeleteBrandAsync(Brand brand)
        {
            return _brandRepo.DeleteBrandAsync(brand);
        }

        public Task<PagedResult<Brand>> GetBrandsByPagingAsync(PagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest), "Paging request cannot be null");
            }

            return _brandRepo.GetBrandsPagingAsync(pagingRequest);
        }

        public Task<PagedResult<Brand>> GetBrandsByPagingAsync(string searchName, PagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest), "Paging request cannot be null");
            }

            return _brandRepo.GetBrandsPagingAsync(searchName, pagingRequest);
        }
    }   
}
    