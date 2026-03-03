using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static RgbRando.RgbRandoMod;

namespace RgbRando
{
    public class TestColors
    {
        public static float countingRed = 0.0f;
        public static float countingGreen = 0.0f;
        public static float countingBlue = 0.0f;

        public static void UpdateColors()
        {
            PostProcess.material.SetFloat("RedPercent", countingRed);
            PostProcess.material.SetFloat("BluePercent", countingBlue);
            PostProcess.material.SetFloat("GreenPercent", countingGreen);
        }

        public static void ColorTestMethod()
        {
            if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.U))
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    countingRed += 0.2f;
                    if (countingRed >= 1)
                    {
                        countingRed = 0;
                        countingGreen += 0.2f;
                        if (countingGreen >= 1)
                        {
                            countingGreen = 0;
                            countingBlue += 0.2f;
                            if (countingBlue >= 1)
                            {
                                countingBlue = 0;
                            }
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.U))
                {
                    countingRed = Mathf.Min(1, countingRed + 0.2f);
                    if (countingRed >= 1)
                    {
                        countingGreen = Mathf.Min(1, countingGreen + 0.2f);
                        if (countingGreen >= 1)
                        {
                            countingBlue = Mathf.Min(1, countingBlue + 0.2f);
                        }
                    }
                }

                PostProcess.material.SetFloat("RedPercent", countingRed);
                PostProcess.material.SetFloat("BluePercent", countingBlue);
                PostProcess.material.SetFloat("GreenPercent", countingGreen);

                var curGeo = HeroController.instance.playerData.geo;
                HeroController.instance.playerData.geo = 0;
                HeroController.instance.AddGeo((int)(countingRed * 1000) + (int)(countingGreen * 100) + (int)(countingBlue * 10));
            }
        }
    }
}
