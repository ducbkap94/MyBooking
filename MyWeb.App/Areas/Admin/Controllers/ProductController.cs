using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Product;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;
using MyWeb.Service.Services;

namespace MyWeb.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var products = await _productService.GetProductsByPagingAsync(request);
            if (products == null)
            {
                return View();
            }
            return View(products);
        }
        public async Task<IActionResult> Form(int? id)
        {
            var viewModel = await _productService.GetProductFormViewModelAsync(id);
            return PartialView("Partials/_productForm", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductRequest request)
        {
            if (_productService.GetProductByIdAsync(request.Id).Result != null)
            {
                var result = await _productService.UpdateProductAsync(request);
                if (result)
                {
                    return Json(new { Success = true, Message = "Cập nhật nhật thành công" });
                }
                return Json(new { Success = false, Message = "Cập nhật thất bại." });
            }
            else
            {
                var result = await _productService.CreateProductAsync(request);
                if (result)
                {
                    return Json(new { Success = true, Message = "Thêm mới thành công." });
                }
                return Json(new { Success = false, Message = "Thêm mới thất bại." });
            }




        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var product = await _productService.GetProductsByPagingAsync(request);
            return PartialView("Partials/_tableProduct", product);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(product);
            return Ok(new { success = true, message = "Xoá sản phẩm thành công" });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var products = await _productService.GetAllProductsAsync(request);
            if (request == null || !request.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm nào" });
            }

            var result = await _productService.DeleleteSelectedProductsAsync(products.ToList());
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

            var product = await _productService.SearchProductsByNameAsync(searchName, request);
            return PartialView("Partials/_tableProduct", product);
        }
    }
}