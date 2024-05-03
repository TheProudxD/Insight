using UnityEngine;

namespace Extensions
{
    public static class IntExtensions
    {
        public static Vector3 GetVectorFromAngle(this int angle)
        {
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        // Returns 00-FF, value 0->255
        public static string Dec_to_Hex(this int value) => value.ToString("X2");
        
        public static bool BetweenStrictly(this int target, int min, int max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }
            return target > min && target < max;
        }
        
        public static bool Between(this int target, int min, int max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }
            return target >= min && target < max;
        }
        
        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => !IsEven(value);
    }
}