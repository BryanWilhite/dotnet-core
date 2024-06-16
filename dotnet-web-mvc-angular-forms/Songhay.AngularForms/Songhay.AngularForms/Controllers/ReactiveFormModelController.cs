using Microsoft.AspNetCore.Mvc;

namespace Songhay.AngularForms.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReactiveFormModelController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] ReactiveFormModel data)
        {
            if (data == null) return this.BadRequest($"The expected {nameof(data)} is not here.");

            foreach (var phone in data.Phones)
            {
                phone.IsVerified = true;
            }

            data.ServerData = "Yay! We received your submission ✅";

            return this.Ok(data);
        }
    }

    public class ReactiveFormModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public PhoneNumber[] Phones { get; set; }

        public string ServerData { get; set; }
    }

    public class PhoneNumber
    {
        public int Area { get; set; }

        public int Prefix { get; set; }

        public int Line { get; set; }

        public bool IsVerified { get; set; }
    }
}
