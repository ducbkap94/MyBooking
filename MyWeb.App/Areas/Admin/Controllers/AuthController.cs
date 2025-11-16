using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Request;
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
            UserLoginRequest userLogin = new UserLoginRequest
            {
                Username = username,
                Password = password
            };
            var user = await _authService.GetUserByIdAsync(userLogin);
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View(); // hoặc trả JSON nếu dùng AJAX
            }
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.Username),
                new Claim("Email", user.Email ?? ""),
                new Claim("FullName", user.FullName ?? "")
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        
        public async Task<IActionResult> Logout()
        {
            // Logic for logging out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}