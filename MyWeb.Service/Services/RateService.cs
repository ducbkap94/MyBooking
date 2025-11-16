using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.RentalRate;
using MyWeb.Business.Dtos.RentalRate.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;

namespace MyWeb.Service.Services
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateRepository;
        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<JsonResult> CreateRateAsync(RentalRateRequest request)
        {
            var result = await _rateRepository.CreateRateAsync(new RentalRate
            {
                RateId = request.RateId,
                ProductId = request.ProductId,
                PricePerDay = request.PricePerDay,
                PricePerHour = request.PricePerHour,
                StartDate = request.StartDate,
                EndDate = request.EndDate,

            });
            if (result)
            {
                return new JsonResult(new { Success = true, Message = "Thêm mới thành công." });
            }
            return new JsonResult(new { Success = false, Message = "Thêm mới thất bại." });

        }

        public Task<JsonResult> CreateRateAsync(RentalRate request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRateAllAsync(List<RentalRate> rentalRates)
        {
            return _rateRepository.DeleteRateAllAsync(rentalRates);
        }

        public Task<bool> DeleteRateAsync(RentalRate rentalRate)
        {
            return _rateRepository.DeleteRateAsync(rentalRate);
        }

        public Task<List<RentalRate>> GetAllRateAsync(int[] ids)
        {
            return _rateRepository.GetAllRateAsync(ids);
        }

        public  Task<RentalRate> GetRateByIdAsync(int id)
        {
            return _rateRepository.GetRateByIdAsync(id);
        }

        public Task<RentalRateFromResponse> GetRateFormResponseAsync(int? id = null)
        {
            return _rateRepository.GetRateFormResponseAsync(id);
        }

        public async Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(PagingRequest request)
        {
            return await _rateRepository.GetRatePagingAsync(request);
        }

        public Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(string searchName, PagingRequest request)
        {
            return _rateRepository.GetRatePagingAsync(searchName, request);
        }

        public async Task<bool> UpdateRateAsync(RentalRateRequest request)
        {
            var rentalRate = await _rateRepository.GetRateByIdAsync(request.RateId);
            if (rentalRate == null)
            {
                return false; ;
            }
            rentalRate.ProductId = request.ProductId;
            rentalRate.PricePerDay = request.PricePerDay;
            rentalRate.PricePerHour = request.PricePerHour;
            rentalRate.StartDate = request.StartDate;
            rentalRate.EndDate = request.EndDate;
            return await _rateRepository.UpdateRateAsync(rentalRate) != null;
        }


    }
}