using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Songhay.ValidationWithMarkup.Web.Models;

namespace Songhay.ValidationWithMarkup.Web.Controllers;

public class HomeController : Controller
{
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger?.LogInformation("Method: {Name}", nameof(Index));

        return View();
    }

    public IActionResult Privacy()
    {
        _logger?.LogInformation("Method: {Name}", nameof(Privacy));

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    readonly ILogger<HomeController> _logger;
}
