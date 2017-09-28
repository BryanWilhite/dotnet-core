namespace Songhay.LightBulbs.Models
{
    public struct Person
    {
        public Person(int ordinal, int moduloDivisor)
        {
            this.Ordinal = ordinal;
            this.ModuloDivisor = moduloDivisor;
        }

        public int Ordinal { get; private set; }

        public int ModuloDivisor { get; private set; }
    }
}