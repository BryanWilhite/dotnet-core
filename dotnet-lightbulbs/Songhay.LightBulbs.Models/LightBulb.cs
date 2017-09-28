namespace Songhay.LightBulbs.Models
{
    public class LightBulb
    {
        public LightBulb(int ordinal, bool isOn)
        {
            this.Ordinal = ordinal;
            this.IsOn = isOn;
        }

        public int Ordinal { get; private set; }

        public bool IsOn { get; set; }
    }
}