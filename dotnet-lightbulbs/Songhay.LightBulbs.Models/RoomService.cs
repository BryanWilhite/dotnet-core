using Songhay.LightBulbs.Models.Extensions;
using System.Collections.Generic;

namespace Songhay.LightBulbs.Models
{
    public class RoomService: IRoomService
    {
        public IEnumerable<LightBulb> SwitchLightsWithAllPersons(int numberOfLightBulbs, bool bulbsOnByDefault, int numberOfPersons)
        {
            var room = (new Room())
                .WithLightBulbs(numberOfLightBulbs, bulbsOnByDefault)
                .WithPersons(numberOfPersons);

            room.SwitchWithAllPersons();

            return room.LightBulbs;
        }
    }
}