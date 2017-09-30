using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Songhay.LightBulbs.Models;

namespace Songhay.LightBulbs.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class LightBulbsController : Controller
    {
        public LightBulbsController(IRoomService roomService)
        {
            this._roomService = roomService;
        }

        [HttpGet("{numberOfLightBulbs}/persons/{numberOfPersons}")]
        public IEnumerable<LightBulb> Get(int numberOfLightBulbs, int numberOfPersons)
        {
            var bulbsOnByDefault = true;
            return this._roomService.SwitchLightsWithAllPersons(numberOfLightBulbs, bulbsOnByDefault,numberOfPersons);
        }

        IRoomService _roomService;
    }
}
