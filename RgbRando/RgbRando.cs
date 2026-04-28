using System;
using Modding;
using UnityEngine;
using Modding.Utils;
using System.Reflection;
using RgbRando.Rando;

namespace RgbRando
{
    public class RgbRandoMod : Mod, ILocalSettings<LocalSettings>, IGlobalSettings<GlobalSettings>
    {
        private static RgbRandoMod _instance;

        private static Shader GrayscaleShader;

        public static LocalSettings LS { get; set; } = new();
        public static GlobalSettings GS { get; set; } = new();

        internal static RgbRandoMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(RgbRandoMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public RgbRandoMod() : base("RgbRando")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            LoadAssets();
            // This is done to make sure material is iniitalized.

            RandoInterop.Hook();

            Log("Initialized");
        }

        public static void EditCamera(On.GameManager.orig_OnNextLevelReady orig, GameManager self)
        {
            orig(self);
            GameCameras.instance.mainCamera.gameObject.GetOrAddComponent<PostProcess>();
            GameCameras.instance.hudCamera.gameObject.GetOrAddComponent<PostProcess>();

            var red = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get("Color-Red");
            var green = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get("Color-Green");
            var blue = RandomizerMod.RandomizerMod.RS.TrackerData.pm.Get("Color-Blue");

            var colorValues = new float[] { (float)red / (float)RgbRandoMod.LS.perColorCount, (float)green / (float)RgbRandoMod.LS.perColorCount, (float)blue / (float)RgbRandoMod.LS.perColorCount };
            RgbRandoMod.Instance.Log($"(Edit Camera) Color parts are {colorValues[0]} {colorValues[1]} {colorValues[2]}");
            PostProcess.material.SetFloat("RedPercent", colorValues[0]);
            PostProcess.material.SetFloat("GreenPercent", colorValues[1]);
            PostProcess.material.SetFloat("BluePercent", colorValues[2]);

        }

        public static void RestoreCamera(On.GameManager.orig_OnNextLevelReady orig, GameManager self)
        {
            PostProcess.material.SetFloat("RedPercent", 1f);
            PostProcess.material.SetFloat("GreenPercent", 1f);
            PostProcess.material.SetFloat("BluePercent", 1f);

            On.GameManager.OnNextLevelReady -= RgbRandoMod.RestoreCamera;
        }

        public void LoadAssets()
        {
            var platform = Application.platform switch
            {
                RuntimePlatform.WindowsPlayer => "windows",
                RuntimePlatform.OSXPlayer => "osx",
                _ => throw new PlatformNotSupportedException("What platform are you even on??")
            };

            var assetBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"RgbRando.grayscaler_{platform}.unity3d"));
            GrayscaleShader = assetBundle.LoadAsset<Shader>("assets/shaders/grayscaler.shader");
        }

        public void Unload()
        {
            On.GameManager.OnNextLevelReady -= EditCamera;
        }

        public void OnLoadLocal(LocalSettings s) => LS = s;
        public LocalSettings OnSaveLocal() => LS;
        public void OnLoadGlobal(GlobalSettings s) => GS = s;
        public GlobalSettings OnSaveGlobal() => GS;


        public class PostProcess : MonoBehaviour
        {
            public Shader shader;
            public static Material material;

            void OnEnable()
            {
                if (shader == null)
                {
                    shader = RgbRandoMod.GrayscaleShader;
                }

                if (material == null)
                {
                    material = new Material(shader);
                }
            }

            void OnRenderImage(RenderTexture source, RenderTexture destination)
            {
                if (material == null)
                {
                    Graphics.Blit(source, destination);
                }

                Graphics.Blit(source, destination, material);
            }
        }
    }
}
