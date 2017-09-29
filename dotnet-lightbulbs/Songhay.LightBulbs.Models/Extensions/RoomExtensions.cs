using System.Linq;

namespace Songhay.LightBulbs.Models.Extensions
{
    /// <summary>
    /// Extensions of <see cref="Room" />
    /// </summary>
    public static class RoomExtensions
    {
        /// <summary>
        /// Returns <see cref="Room" /> with the specified number of Light Bulbs.
        /// </summary>
        /// <param name="room">the Room</param>
        /// <param name="numberOfLightBulbs">number of light bulbs</param>
        /// <param name="bulbsOnByDefault"><c>true</c> when light bulbs are on by default</param>
        /// <remarks>
        /// This strategy returns an array in order to track changes
        /// by reference to <see cref="LightBulb" />.
        /// </remarks>
        public static Room WithLightBulbs(this Room room, int numberOfLightBulbs = 100, bool bulbsOnByDefault = true)
        {
            if (room == null) return null;

            room.LightBulbs = Enumerable.Range(1, numberOfLightBulbs)
                .Select(i => new LightBulb(i, bulbsOnByDefault))
                .ToArray();

            return room;
        }

        /// <summary>
        /// Returns <see cref="Room" /> with the specified number of Persons.
        /// </summary>
        /// <param name="room">the Room</param>
        /// <param name="numberOfPersons">the number of persons</param>
        /// <returns></returns>
        public static Room WithPersons(this Room room, int numberOfPersons = 100)
        {
            if (room == null) return null;

            room.Persons = Enumerable.Range(1, numberOfPersons)
                .Select(i => new Person(i, i));

            return room;
        }

        /// <summary>
        /// Switches light bulbs with all persons.
        /// </summary>
        /// <param name="room"></param>
        public static void SwitchWithAllPersons(this Room room)
        {
            if (room == null) return;

            foreach (var person in room.Persons.Skip(1))
            {
                var bulbs = room.LightBulbs
                    .Where(i =>((i.Ordinal % person.Ordinal) == 0));
                foreach (var bulb in bulbs) bulb.IsOn = !bulb.IsOn;
            }
        }
    }
}