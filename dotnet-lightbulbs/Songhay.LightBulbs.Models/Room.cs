namespace Songhay.LightBulbs.Models
{
    public class Room
    {
        public Room(int numberOfLightBulbs = 90, int numberOfPersons = 9, bool bulbsOnByDefault = true)
        {
            this.LightBulbs = Enumerable.Range(1, numberOfLightBulbs)
                .Select(i => new LightBulb(i, bulbsOnByDefault))
                .ToArray();
            this.Persons = Enumerable.Range(1, numberOfPersons)
                .Select(i => new Person(i, i));
        }

        public LightBulb[] LightBulbs { get; private set; }

        public IEnumerable<Person> Persons { get; private set; }

        public void SwitchWithAllPersons()
        {
            foreach (var person in this.Persons.Skip(1))
            {
                var bulbs = this.LightBulbs
                    .Where(i => ((i.Ordinal % person.Ordinal) == 0));
                foreach (var bulb in bulbs) bulb.IsOn = !bulb.IsOn;
            }
        }
    }
}