using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;
using RgbRando.Rando;
using System;
using static RgbRando.RgbRandoMod;

namespace RgbRando.IC
{
    public class ColorItem : AbstractItem
    {
        public ColorItem(RGB rgb)
        {
            name = Consts.NameByValue(rgb);

            InteropTag tag = RandoInterop.AddTag(this);

            tag.Properties["PinSprite"] = new EmbeddedSprite("colorWheelExtra");

            UIDef = new MsgUIDef
            {
                name = new BoxedString(RandoInterop.Clean(name)),
                shopDesc = new BoxedString("A piece of color! Maybe you could use that in a printer?"),
                sprite = new EmbeddedSprite("colorWheelExtra"),
            };
        }

        public override void GiveImmediate(GiveInfo info)
        {
            var colorItemTerm = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get(name) + 1;
            float partCompleted = (float)colorItemTerm / (float)RgbRandoMod.LS.ItemCountByType();
            RgbRandoMod.Instance.Log($"Color for {name} is {colorItemTerm}, and part completed is {partCompleted}");

            switch (name)
            {
                case "Color-Red":
                    PostProcess.material.SetFloat("RedPercent", partCompleted);
                    break;
                case "Color-Green":
                    PostProcess.material.SetFloat("GreenPercent", partCompleted);
                    break;
                case "Color-Blue":
                    PostProcess.material.SetFloat("BluePercent", partCompleted);
                    break;
            }
        }
    }
}
