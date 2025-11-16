using MyWeb.Data.Models;
using MyWeb.Common.Paging;
using MyWeb.Business.Dtos.Asset.Response;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Business.Dtos.RentalRate;
using MyWeb.Data.Repositories;
using MyWeb.Business.Dtos.RentalRate.Response;
namespace MyWeb.Data.IRepositories
{
    public interface IRateRepository
    {
        Task<RentalRate?> GetRateByIdAsync(int id);
        Task<List<RentalRate>> GetAllRateAsync(int[] ids);
        Task<IEnumerable<RentalRate>> GetAllRateAsync();
        Task<bool> CreateRateAsync(RentalRate rentalRate);
        Task<RentalRate> UpdateRateAsync(RentalRate rentalRate);
        Task<bool> DeleteRateAllAsync(List<RentalRate> rentalRate);
        Task<bool> DeleteRateAsync(RentalRate assrentalRateet);
        Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(PagingRequest request);
        Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(string searchName, PagingRequest request);
        Task<RentalRateFromResponse> GetRateFormResponseAsync(int? id = null);
    }
}