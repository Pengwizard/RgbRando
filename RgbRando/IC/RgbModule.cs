using ItemChanger.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mono.Security.X509.X520;
using static RgbRando.RgbRandoMod;

namespace RgbRando.IC
{
    internal class RgbModule : Module
    {
        public override void Initialize()
        {
            //var red = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get(Consts.NameByValue(RGB.red));
            //var green = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get(Consts.NameByValue(RGB.green));
            //var blue = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get(Consts.NameByValue(RGB.blue));
            //RgbRandoMod.ColorValues = new float[] { red / RgbRandoMod.LS.perColorCount, green / RgbRandoMod.LS.perColorCount, blue / RgbRandoMod.LS.perColorCount };
            //RgbRandoMod.Instance.Log($"(Initialize) Color parts are {ColorValues[0]} {ColorValues[1]} {ColorValues[2]}");
            // 
            On.GameManager.OnNextLevelReady += RgbRandoMod.EditCamera;
            On.GameManager.OnNextLevelReady -= RgbRandoMod.RestoreCamera;
        }

        public override void Unload()
        {
            On.GameManager.OnNextLevelReady -= RgbRandoMod.EditCamera;
            On.GameManager.OnNextLevelReady += RgbRandoMod.RestoreCamera;
        }
    }
}
