using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb.App.Areas.Admin.Controllers;

public class HomeController : Controller
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }


}
