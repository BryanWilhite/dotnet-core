using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Songhay.AngularForms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormlyController : ControllerBase
    {
        [HttpGet]
        public JObject Get()
        {
            var anon = new
            {
                componentSet = new
                {
                    form1 = new
                    {
                        fields = JArray.Parse("[]").AddForm1Configuration(),
                        model = new object()
                    },

                    form2 = new
                    {
                        fields = JArray.Parse("[]").FillWithForm2Configuration(),
                        model = new object()
                    },

                    form3 = new
                    {
                        fields = JArray.Parse("[]").FillWithForm3Configuration(),
                        model = new object()
                    },
                },

                model = new object()
            };

            return JObject.FromObject(anon);
         }
    }
}