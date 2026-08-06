using System.IO;
using ItemChanger;
using ItemChanger.Tags;
using Modding;
using RandomizerMod.Logging;
using RandomizerMod.RandomizerData;
using RandomizerMod.RC;
using RgbRando.IC;

namespace RgbRando.Rando
{
    internal static class RandoInterop
    {
        public static void Hook()
        {
            RandoMenuPage.Hook();
            RequestModifier.Hook();
            LogicAdder.Hook();

            RandoController.OnExportCompleted += AddRgbModule;
            SettingsLog.AfterLogSettings += LogRandoSettings;

            DefineItems();

            if (ModHooks.GetMod("RandoSettingsManager") is Mod)
            {
                RSMInterop.Hook();
            }

            if(ModHooks.GetMod("ConnectionSettingsRando") is Mod)
            {
                CSRInterop.Hook();
            }
        }

        private static void AddRgbModule(RandoController controller)
        {
            if (!RgbRandoMod.GS.Enabled)
            {
                return;
            }

            ItemChangerMod.Modules.GetOrAdd<RgbModule>();
        }

        private static void LogRandoSettings(LogArguments args, TextWriter w) {
            w.WriteLine("Logging RgbRando settings:");
            w.WriteLine(JsonUtil.Serialize(RgbRandoMod.GS));
        }

        public static void DefineItems()
        {
            ColorItem colorItemR = new(RGB.red);
            Finder.DefineCustomItem(colorItemR);

            ColorItem colorItemG = new(RGB.green);
            Finder.DefineCustomItem(colorItemG);

            ColorItem colorItemB = new(RGB.blue);
            Finder.DefineCustomItem(colorItemB);
        }

        public static string Clean(string name)
        {
            return name.Replace("-", " - ");
        }

        public static InteropTag AddTag(TaggableObject obj)
        {
            InteropTag tag = obj.GetOrAddTag<InteropTag>();
            tag.Message = "RandoSupplementalMetadata";
            tag.Properties["ModSource"] = RgbRandoMod.Instance.GetName();

            return tag;
        }
    }
}
