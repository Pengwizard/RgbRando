using ConnectionSettingsRando;

namespace RgbRando.Rando
{
    internal class CSRInterop
    {
        public static void Hook()
        {
            CSR.Register(
                RgbRandoMod.Instance.GetName(),
                () => RgbRandoMod.GS,
                s => SettingsRandomizer.CopyTo(s, RgbRandoMod.GS)
            );
        }
    }
}
