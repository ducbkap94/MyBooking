using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.RentalRate;
using MyWeb.Business.Dtos.RentalRate.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Service.Interfaces
{
    
    public interface IRateService
    {
        Task<RentalRate> GetRateByIdAsync(int id);
        Task<List<RentalRate>> GetAllRateAsync(int[] ids); 
        Task<JsonResult> CreateRateAsync(RentalRateRequest request);
        Task<bool> UpdateRateAsync(RentalRateRequest rentalRate);
        Task<bool> DeleteRateAllAsync(List<RentalRate> rentalRates);
        Task<bool> DeleteRateAsync(RentalRate rentalRate);
        Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(PagingRequest request);
        Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(string searchName, PagingRequest request);
        Task<RentalRateFromResponse> GetRateFormResponseAsync(int? id = null);
        
    }
}