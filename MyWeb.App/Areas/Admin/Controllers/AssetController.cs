using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Common.Paging;
using MyWeb.Service.Interfaces;

namespace MyWeb.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetsService;
        public AssetController(IAssetService assetService)
        {
            _assetsService = assetService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var assets = await _assetsService.GetAssetsPagingAsync(request);
            if (assets == null)
            {
                return View();
            }
            return View(assets);
        }
        public async Task<IActionResult> Form(int id)
        {
            
            var viewModel = await _assetsService.GetAssetFormResponseAsync(id);
            return PartialView("Partials/_assetForm", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Save(AssetRequest request)
        {
            if (_assetsService.GetAssetByIdAsync(request.AssetId).Result != null)
            {
                return await _assetsService.UpdateAssetAsync(request);
            }
            else
            {
                return await _assetsService.CreateAssetAsync(request);
                
            }




        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var product = await _assetsService.GetAssetsPagingAsync(request);
            return PartialView("Partials/_tableAsset", product);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _assetsService.GetAssetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _assetsService.DeleteAssetAsync(product);
            return Ok(new { success = true, message = "Xoá sản phẩm thành công" });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var products = await _assetsService.GetAllAssetsAsync(request);
            if (request == null || !request.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm nào" });
            }

            var result = await _assetsService.DeleteAssetAllAsync(products.ToList());
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

            var product = await _assetsService.GetAssetsPagingAsync(searchName, request);
            return PartialView("Partials/_tableAsset", product);
        }
    }
}