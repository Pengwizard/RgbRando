using ItemChanger;
using ItemChanger.Tags;
using Modding;
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

            DefineItems();

            if (ModHooks.GetMod("RandoSettingsManager") is Mod)
            {
                RSMInterop.Hook();
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
