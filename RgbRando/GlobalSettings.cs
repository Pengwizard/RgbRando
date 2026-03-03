
namespace RgbRando
{
    public class GlobalSettings
    {
        public bool Enabled;

        [MenuChanger.Attributes.MenuRange(1, 100)]
        public int PartsPerColor = 3;
    }
}
