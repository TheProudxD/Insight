using UnityEngine;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static Color RandomColor => new(Random.value, Random.value, Random.value);

        public static string ToHex(this Color c)
        {
            Color32 c32 = c;
            return c32.ToHex();
        }

        public static Color SetA(this Color c, float a)
        {
            c.a = a;
            return c;
        }

        // Sets out values to Hex String 'FF'
        public static void GetHexFromColor(this Color color, out string red, out string green, out string blue,
            out string alpha)
        {
            red = color.r.Dec01_to_Hex();
            green = color.g.Dec01_to_Hex();
            blue = color.b.Dec01_to_Hex();
            alpha = color.a.Dec01_to_Hex();
        }
    }
}
