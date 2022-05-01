using Microsoft.AspNetCore.Mvc;

namespace Songhay.ValidationWithAjax.Web.Controllers;

public class Todos : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}