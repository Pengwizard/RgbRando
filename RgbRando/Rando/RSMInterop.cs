using RandoSettingsManager;
using RandoSettingsManager.SettingsManagement;
using RandoSettingsManager.SettingsManagement.Versioning;

namespace RgbRando.Rando
{
    internal static class RSMInterop
    {
        public static void Hook()
        {
            RandoSettingsManagerMod.Instance.RegisterConnection(new YarcSettingsProxy());
        }
    }

    internal class YarcSettingsProxy : RandoSettingsProxy<GlobalSettings, string>
    {
        public override string ModKey => RgbRandoMod.Instance.GetName();

        public override VersioningPolicy<string> VersioningPolicy { get; } = new EqualityVersioningPolicy<string>(RgbRandoMod.Instance.GetVersion());

        public override void ReceiveSettings(GlobalSettings settings)
        {
            settings ??= new();
            RandoMenuPage.Instance.rgbMEF.SetMenuValues(settings);
        }

        public override bool TryProvideSettings(out GlobalSettings settings)
        {
            settings = RgbRandoMod.GS;
            return settings.Enabled;
        }
    }
}