using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mono.Security.X509.X520;

namespace RgbRando
{
    public enum RGB
    {
        red,
        green,
        blue
    }

    public class Consts
    {
        public static string BaseColorItemName = "Color-";
        public static string RedColorItemName = BaseColorItemName + "Red";
        public static string GreenColorItemName = BaseColorItemName + "Green";
        public static string BlueColorItemName = BaseColorItemName + "Blue";

        public readonly static RGB[] colorValues = { RGB.red, RGB.green, RGB.blue };


        public static string NameByValue(RGB value)
        {
            return value switch
            {
                RGB.red => Consts.RedColorItemName,
                RGB.green => Consts.GreenColorItemName,
                RGB.blue => Consts.BlueColorItemName,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
