using MenuChanger;
using MenuChanger.Extensions;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using RandomizerMod.Menu;
using static RandomizerMod.Localization;

namespace RgbRando
{
    public class RandoMenuPage
    {
        internal MenuPage RgbRandoPage;
        internal MenuElementFactory<GlobalSettings> rgbMEF;
        internal VerticalItemPanel rgbVIP;

        internal SmallButton JumpToRgbButton;

        internal static RandoMenuPage Instance { get; private set; }

        public static void OnExitMenu()
        {
            Instance = null;
        }

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(ConstructMenu, HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private static bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            button = Instance.JumpToRgbButton;
            return true;
        }

        private void SetTopLevelButtonColor()
        {
            if (JumpToRgbButton != null)
            {
                JumpToRgbButton.Text.color = RgbRandoMod.GS.Enabled ? new UnityEngine.Color(0, 1, 0) : new UnityEngine.Color(1, 0, 0);
            }
        }

        private static void ConstructMenu(MenuPage landingPage)
        {
            Instance = new(landingPage);
        }

        private RandoMenuPage(MenuPage landingPage)
        {
            RgbRandoPage = new MenuPage(Localize("RgbRando"), landingPage);
            rgbMEF = new(RgbRandoPage, RgbRandoMod.GS);
            rgbVIP = new(RgbRandoPage, new(0, 300), 75f, true, rgbMEF.Elements);
            Localize(rgbMEF);
            foreach (IValueElement e in rgbMEF.Elements)
            {
                e.SelfChanged += obj => SetTopLevelButtonColor();
            }

            JumpToRgbButton = new(landingPage, Localize("RgbRando"));
            JumpToRgbButton.AddHideAndShowEvent(landingPage, RgbRandoPage);
            SetTopLevelButtonColor();
        }
    }
}