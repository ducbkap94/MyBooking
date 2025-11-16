using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Common.Paging;
using MyWeb.Service.Interfaces;

namespace MyWeb.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AddBookingController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ICustomerService _customerService;
        public AddBookingController(IAssetService assetService, ICustomerService customerService)
        {
            _assetService = assetService;
            _customerService= customerService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> SearchAsset(String? searchName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var assets = await _assetService.GetAllAssetSelectAsync(searchName, startDate, endDate);
            return PartialView("Partials/_tableAsset", assets);
        }
          public async Task<IActionResult> Search(String? searchName = null, int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var cus = await _customerService.GetCustomerPagingAsync(searchName, request);
            return PartialView("Partials/_tableCustomer", cus);
        }
    }
}