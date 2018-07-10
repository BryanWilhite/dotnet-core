using Microsoft.AspNetCore.Mvc;

namespace Songhay.WebApiOne.Controllers
{
    [Route("[controller]")]
    public class MyViewController : Controller
    {
        [Route("one")]
        public IActionResult GetViewOne()
        {
            return this.View("ViewOne");
        }
    }
}