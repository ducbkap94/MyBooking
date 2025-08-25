using Microsoft.AspNetCore.Mvc;
using MyWeb.Service;
namespace MyWeb.App.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            // Logic for displaying the login page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            
            var token = await _authService.LoginAsync(username, password);
            Console.WriteLine($"Token: {token}");
            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View(); // hoặc trả JSON nếu dùng AJAX
            }

            // Lưu token vào cookie (nếu cần)
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public IActionResult Logout()
        {
            // Logic for logging out the user
            return RedirectToAction("Login");
        }
    }
}