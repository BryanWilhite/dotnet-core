using System.Collections.Generic;

namespace Songhay.LightBulbs.Models
{
    public interface IRoomService
    {
         IEnumerable<LightBulb> SwitchLightsWithAllPersons(int numberOfLightBulbs, bool bulbsOnByDefault,
        int numberOfPersons);
    }
}