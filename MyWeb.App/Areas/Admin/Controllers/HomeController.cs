using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb.App.Areas.Admin.Controllers;

public class HomeController : Controller
{
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }


}
