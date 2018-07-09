using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Songhay.WebApiOne.Controllers
{
    [Route("[controller]")]
    public class ViewController : Controller
    {
        [Route("one")]
        IActionResult GetOne()
        {
            return this.View();
        }
    }
}