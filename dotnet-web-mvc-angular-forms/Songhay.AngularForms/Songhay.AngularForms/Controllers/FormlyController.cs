using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Songhay.AngularForms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormlyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var anon = new
            {
                componentSet = new
                {
                    form1 = new
                    {
                        fields = FormlyUtility.GetForm1Configuration(),
                    },

                    form2 = new
                    {
                        fields = FormlyUtility.GetForm2Configuration(),
                    },

                    form3 = new
                    {
                        fields = FormlyUtility.GetForm3Configuration(),
                    },
                },
            };

            var json = JsonSerializer.Serialize(anon);

            return this.Content(json, "application/json");
         }
    }
}