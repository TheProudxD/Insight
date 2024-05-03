using System;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime EpochTime = new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimestamp(this DateTime dateTime) => (long)dateTime.Subtract(EpochTime).TotalSeconds;
        
        public static DateTime FromUnixTimestamp(this long unixTimestamp) => EpochTime.AddSeconds(unixTimestamp);
        
        public static bool Between(this TimeSpan target, TimeSpan min, TimeSpan max) => target >= min && target < max;

        public static bool Between(this DateTime target, DateTime min, DateTime max) => target >= min && target < max;
    }
}
