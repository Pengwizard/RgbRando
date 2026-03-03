using RandomizerMod.RC;

namespace RgbRando.Rando
{
    public class RequestModifier
    {
        public static void Hook()
        {
            RequestBuilder.OnUpdate.Subscribe(-499, SetupItems);
            RequestBuilder.OnUpdate.Subscribe(-499, DefinePools);
            RequestBuilder.OnUpdate.Subscribe(0, CloneSettings);
        }

        private static void SetupItems(RequestBuilder rb)
        {
            if (!RgbRandoMod.GS.Enabled)
            {
                return;
            }

            foreach (RGB rgb in Consts.colorValues)
            {
                rb.EditItemRequest(Consts.NameByValue(rgb), info =>
                {
                    info.getItemDef = () => new RandomizerMod.RandomizerData.ItemDef()
                    {
                        Name = Consts.NameByValue(rgb),
                        Pool = "RGB",
                        MajorItem = false,
                        PriceCap = 255
                    };
                });

                rb.AddItemByName(Consts.NameByValue(rgb), RgbRandoMod.GS.PartsPerColor);
            }
        }

        private static void DefinePools(RequestBuilder rb)
        {
            if (!RgbRandoMod.GS.Enabled)
            {
                return;
            }
            
            ItemGroupBuilder group = null;
            string label = RBConsts.SplitGroupPrefix + "Rgb";
            foreach (ItemGroupBuilder igb in rb.EnumerateItemGroups())
            {
                if (igb.label == label)
                {
                    group = igb;
                    break;
                }
            }
            group ??= rb.MainItemStage.AddItemGroup(label);

            rb.OnGetGroupFor.Subscribe(0.01f, ResolveRgbGroup);
            bool ResolveRgbGroup(RequestBuilder rb, string item, RequestBuilder.ElementType type, out GroupBuilder gb)
            {
                gb = default;
                return false;
            }
        }

        private static void CloneSettings(RequestBuilder rb)
        {
            LocalSettings l = RgbRandoMod.LS;
            GlobalSettings g = RgbRandoMod.GS;

            l.perColorCount = g.PartsPerColor;
        }
    }
}
