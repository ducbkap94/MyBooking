using Microsoft.AspNetCore.Mvc;
using MyWeb.Service;
using MyWeb.Data.Models;
using System.Threading.Tasks;
using MyWeb.Common.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
namespace MyWeb.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var brands = await _brandService.GetBrandsByPagingAsync(request);

            return View(brands);
        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var brands = await _brandService.GetBrandsByPagingAsync(request);
            return PartialView("Partials/_tablePartial", brands);
        }
        public async Task<IActionResult> Form(int? id)
        {
            if (id.HasValue)
            {
                var brand = await _brandService.GetBrandByIdAsync(id.Value);

                return PartialView("Partials/_brandForm", brand);
            }
            return PartialView("Partials/_brandForm", new Brand());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Brand brand)
        {
            if (brand.Id.ToString() == "" || brand.Id == 0)
            {
                await _brandService.CreateBrandAsync(brand);
                return Ok(new { success = true, message = "Thêm thương hiệu thành công" });
            }
            else
            {
                await _brandService.UpdateBrandAsync(brand);
                return Ok(new { success = true, message = "Cập nhật thương hiệu thành công" });
            }

        }

        public async Task<IActionResult> Delete(int id)
        {

            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            await _brandService.DeleteBrandAsync(brand);
            return Ok(new { success = true, message = "Xoá thương hiệu thành công" });
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchName, int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var brands = await _brandService.GetBrandsByPagingAsync(searchName, request);
            return PartialView("Partials/_tablePartial", brands);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var brands = await _brandService.GetAllBrandsAsync(request);
            if (brands == null || !brands.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy thương hiệu nào" });
            }

            var result = await _brandService.DeleteSelectedBrandsAsync(brands.ToList());
            if (result)
            {
                return Json(new { success = true, message = "Xoá thành công các thương hiệu đã chọn" });
            }
            else
            {
                return Json(new { success = false, message = "Xoá không thành công" });
            }


        }
    }
   
}