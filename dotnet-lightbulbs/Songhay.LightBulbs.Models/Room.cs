using System.Collections.Generic;
using System.Linq;

namespace Songhay.LightBulbs.Models
{
    public class Room
    {
        public LightBulb[] LightBulbs { get; set; }

        public IEnumerable<Person> Persons { get; set; }
    }
}