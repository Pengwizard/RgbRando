using IL.InControl.NativeDeviceProfiles;

namespace RgbRando
{
    public class LocalSettings
    {
        public bool Enabled = false;
        public int perColorCount = 0;

        public int ItemCountByType()
        {
            return perColorCount;
        }
    }
}
