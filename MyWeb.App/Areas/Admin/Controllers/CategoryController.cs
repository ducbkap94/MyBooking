using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;

namespace MyWeb.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var brands = await _categoryService.GetCategoriesByPagingAsync(request);
            if (brands == null)
            {
                return View();
            }
            return View(brands);
        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var cat = await _categoryService.GetCategoriesByPagingAsync(request);
            return PartialView("Partials/_tableCategory", cat);
        }
        public async Task<IActionResult> Form(int Id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(Id);
            return PartialView("Partials/_categoryForm", category); 
        }
        public async Task<IActionResult> Save(Category category)
        {
            if (category.CategoryId.ToString() == "" || category.CategoryId == 0)
            {
                await _categoryService.CreateCategoryAsync(category);
                return Ok(new { success = true, message = "Thêm nhóm sản phẩm thành công" });
            }
            else
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok(new { success = true, message = "Cập nhật nhóm sản phẩm thành công" });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {

            var brand = await _categoryService.GetCategoryByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategoryAsync(brand);
            return Ok(new { success = true, message = "Xoá thương hiệu thành công" });
        }
        [HttpPost]
        public async Task<JsonResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var brands = await _categoryService.GetAllCategoriesAsync(request);
            if (brands == null || !brands.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy thương hiệu nào" });
            }

            var result = await _categoryService.DeleteCategoryAllAsync(brands.ToList());
            if (result)
            {
                return Json(new { success = true, message = "Xoá thành công các thương hiệu đã chọn" });
            }
            else
            {
                return Json(new { success = false, message = "Xoá không thành công" });
            }


        }
        public async Task<IActionResult> Search(string searchName, int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var cats = await _categoryService.GetBrandsByPagingAsync(searchName, request);
            return PartialView("Partials/_tableCategory", cats);
        }
    }

}
    