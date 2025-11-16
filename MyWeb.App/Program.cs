
using Microsoft.EntityFrameworkCore;
using MyWeb.Service;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Repositories;
using MyWeb.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyWeb.Service.Interfaces;
using MyWeb.Service.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Registering the IUserRepository/ Registering the IJwtHelper
builder.Services.AddScoped<IAuthService, AuthService>(); // Registering the IAuthService
builder.Services.AddScoped<IBrandService, BrandService>(); // Registering the IBrandService
builder.Services.AddScoped<IBrandRepository, BrandRepository>(); // Registering the IProductService
builder.Services.AddScoped<JwtHelper>(); // Registering JwtHelper
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Registering the ICategoryRepository
builder.Services.AddScoped<ICategoryService, CategoryService>(); // Registering the ICategoryService
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Registering the IUserRepository
builder.Services.AddScoped<IUserService, UserService>(); // Registering the IUserService
builder.Services.AddScoped<IViewRenderService, ViewRenderService>(); // Registering the IViewRenderService
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Registering the IProductRepository
builder.Services.AddScoped<IProductService, ProductService>(); // Registering the IProductService
builder.Services.AddScoped<IAssetRepository, AssetsRepository>(); // Registering the IAssetRepository
builder.Services.AddScoped<IAssetService, AssetService>(); // Registering the IAssetService
builder.Services.AddScoped<IRateRepository, RateRepository>(); // Registering the IRateRepository
builder.Services.AddScoped<IRateService, RateService>(); // Registering the IRateService
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); // Registering the ICustomerRepository
builder.Services.AddScoped<ICustomerService, CustomerService>(); // Registering the ICustomerService
// Registering JwtHelper
builder.Services.AddDbContext<MyWebDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Admin/Auth/Login"; // Đường dẫn đến trang đăng nhập
    options.ExpireTimeSpan = TimeSpan.FromHours(1); 
    options.SlidingExpiration = true; 
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();





app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
