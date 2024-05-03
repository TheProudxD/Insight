using UnityEngine;

namespace Extensions
{
    public static class FloatExtensions
    {
        // Returns a hex string based on a number between 0->1
        public static string Dec01_to_Hex(this float value) => ((int)Mathf.Round(value * 255f)).Dec_to_Hex();
        
        public static bool BetweenStrictly(this float target, float min, float max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }
            return target > min && target < max;
        }
        
        public static bool Between(this float target, float min, float max)
        {
            return target >= min && target < max;
        }
        
        public static bool IsEven(this float value) => value % 2 == 0;

        public static bool IsOdd(this float value) => !IsEven(value);
        
        public static float Cut(this float value, int tail)
        {
            float t = Mathf.Pow(10, tail);
            int intValue = (int)(value * t);

            return intValue / t;
        }
    }
}