
using Microsoft.EntityFrameworkCore;
using MyWeb.Service;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Repositories;
using MyWeb.Data.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Registering the IUserRepository/ Registering the IJwtHelper
builder.Services.AddScoped<IAuthService, AuthService>(); // Registering the IAuthService
builder.Services.AddScoped<IBrandService, BrandService>(); // Registering the IBrandService
builder.Services.AddScoped<IBrandRepository, BrandRepository>(); // Registering the IProductService
builder.Services.AddScoped< JwtHelper>(); // Registering JwtHelper
 // Registering JwtHelper
builder.Services.AddDbContext<MyWebDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
