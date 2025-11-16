namespace MyWeb.App.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyWeb.Business.Dtos.RentalRate;
    using MyWeb.Common.Paging;
    using MyWeb.Service.Interfaces;

    [Area("Admin")]
    public class RateController : Controller
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var assets = await _rateService.GetRatePagingAsync(request);
            if (assets == null)
            {
                return View();
            }
            return View(assets);
        }
        public async Task<IActionResult> Form(int id)
        {
            
            var viewModel = await _rateService.GetRateFormResponseAsync(id);
            return PartialView("Partials/_rateForm", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Save(RentalRateRequest request)
        {
            if (_rateService.GetRateByIdAsync(request.RateId).Result != null)
            {
                var result = await _rateService.UpdateRateAsync(request);
                if (result)
                {
                    return Json(new { Success = true, Message = "Cập nhật nhật thành công" });
                }
                return Json(new { Success = false, Message = "Cập nhật thất bại." });
            }
            else
            {
                return await _rateService.CreateRateAsync(request);
                
            }




        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var product = await _rateService.GetRatePagingAsync(request);
            return PartialView("Partials/_tableRate", product);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _rateService.GetRateByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _rateService.DeleteRateAsync(product);
            return Ok(new { success = true, message = "Xoá giá thuê thành công" });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var products = await _rateService.GetAllRateAsync(request);
            if (request == null || !request.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm nào" });
            }

            var result = await _rateService.DeleteRateAllAsync(products.ToList());
            if (result)
            {
                return Json(new { success = true, message = "Xoá thành công các sản phẩm đã chọn" });
            }
            else
            {
                return Json(new { success = false, message = "Xoá không thành công" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchName, int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var product = await _rateService.GetRatePagingAsync(searchName, request);
            return PartialView("Partials/_tableRate", product);
        }
    }
}