using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.DTOs.User;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
using MyWeb.Service;
using MyWeb.Service.Interfaces;

namespace MyWeb.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;

        public UserController(IViewRenderService viewRenderService, IUserService userService)
        {

            _viewRenderService = viewRenderService;
            _userService = userService;
        }
        public async Task<IActionResult> LoadTable(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var users = await _userService.GetUsersPaging(request);
            return PartialView("Partials/_tableUser", users);
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var users = await _userService.GetUsersPaging(request);
            if (users == null)
            {
                return View();
            }
            return View(users);
        }
        public async Task<IActionResult> Form(int? Id)
        {
            if (Id!=null && Id.Value!=0)
            {
                var user = await _userService.GetUserByIdAsync(Id.Value);
                var request = new RegisterUserRequest
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Gender =(int) user.Gender,
                    Address = user.Address,
                    Roles = user.UserRoles?.Select(ur => ur.Role.Name).ToList(),
                    IdentityNumber = user.IdentityNumber,
                    Status = user.Status,
                    DateOfBirth = user.DateOfBirth,
                    Password = "",
                    TaxCode= user.TaxCode,
                    BankAccount= user.BankAccount,
                    BankName= user.BankName,
                    
                };
                
                return PartialView("Partials/_userForm", request);
            }
            return PartialView("Partials/_userForm", new RegisterUserRequest());
        }


        [HttpPost]
        public async Task<IActionResult> Save(RegisterUserRequest request)
        {
           
            if(_userService.GetUserByIdAsync(request.Id).Result != null)
            {
                var result = await _userService.CreateUserAsync(request);
                if (result)
                {
                    return Json(new { Success = true, Message = "Cập nhật người dùng thành công." });
                }
                return Json(new { Success = false, Message = "Cập nhật người dùng thất bại." });
            }
            else
            {
                 if (!_userService.IsUserAvailableAsync(request).Result.Success)
            {
                return Json(_userService.IsUserAvailableAsync(request).Result);
            }
                var result = await _userService.CreateUserAsync(request);
                if (result)
                {
                    return Json(new { Success = true, Message = "Thêm người dùng thành công." });
                }
                return Json(new { Success = false, Message = "Thêm người dùng thất bại." });
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return Ok(new { success = true, message = "Xoá người dùng thành công." });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchName, int page = 1, int pageSize = 10)
        {
            var request = new PagingRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var users = await _userService.GetUsersByPagingAsync(searchName, request);
            return PartialView("Partials/_tableUser", users);
        }
    }

}
