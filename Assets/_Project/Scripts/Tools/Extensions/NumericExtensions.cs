using System;

namespace Extensions
{
    public static class NumericExtensions
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            T result;
        
            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }
            else
            {
                result = value;
            }
        
            return result;
        }
    }
}
