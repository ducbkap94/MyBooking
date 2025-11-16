using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Business.Dtos.Customer.Request;
using MyWeb.Common.Paging;
using MyWeb.Service.Interfaces;
namespace MyWeb.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var assets = await _customerService.GetCustomerPagingAsync(request);
            if (assets == null)
            {
                return View();
            }
            return View(assets);
        }
        public async Task<IActionResult> Form(int id)
        {

            var viewModel = await _customerService.GetCustomerFormResponseAsync(id);
            return PartialView("Partials/_customerForm", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Save(CustomerRequest request)
        {
            if (_customerService.GetCustomerByIdAsync(request.Id).Result != null)
            {
                return await _customerService.UpdateCustomerAsync(request);
            }
            else
            {
                return await _customerService.CreateCustomerAsync(request);

            }
        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var product = await _customerService.GetCustomerPagingAsync(request);
            return PartialView("Partials/_tableCustomer", product);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _customerService.GetCustomerByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _customerService.DeleteCustomerAsync(product);
            return Ok(new { success = true, message = "Xoá khách hàng thành công" });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSelected(int[] request)
        {
            if (request == null || request.Length == 0)
            {
                return Json(new { success = false, message = "Không có bản ghi nào được chọn" });
            }

            var customers = await _customerService.GetAllCustomersAsync(request);
            if (request == null || !request.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm nào" });
            }

            var result = await _customerService.DeleteCustomerAllAsync(customers.ToList());
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

            var cus = await _customerService.GetCustomerPagingAsync(searchName, request);
            return PartialView("Partials/_tableCustomer", cus);
        }
    }
}