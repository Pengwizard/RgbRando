using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.RC;
using RandomizerMod.Settings;

namespace RgbRando.Rando
{
    public static class LogicAdder
    {
        public static void Hook()
        {
            RCData.RuntimeLogicOverride.Subscribe(50, ApplyLogic);
        }

        private static void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!RgbRandoMod.GS.Enabled)
            {
                return;
            }

            foreach (RGB value in Consts.colorValues)
            {
                var name = Consts.NameByValue(value);
                Term term = lmb.GetOrAddTerm(name, TermType.Int);
                lmb.AddItem(new SingleItem(name, new RandomizerCore.TermValue(term, 1)));
            }
        }
    }
}
