#nullable enable
using System;

namespace SharpStackConvert
{
    public static class TypeConverter
    {
        /// <summary>
        /// This method converts the input to Decimal. If the input is null or empty, it returns 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();

            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            decimal.TryParse(strInput, out var ret);
            return ret;
        }

        /// <summary>
        /// This method converts the input to a double. If the input is null or empty, it returns 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();

            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            double.TryParse(strInput, out var ret);
            return ret;
        }

        /// <summary>
        /// This method converts the input to a string. If the input is null, it returns an empty string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CString(this object? input)
        {
            return input switch
            {
                decimal input1 => input1.ToString("#0.00#"),
                double d => d.ToString("#0.00#"),
                null => "",
                _ => input.ToString()
            };
        }

        /// <summary>
        /// This method converts the input to an integer. If the input is null or empty, it returns 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();

            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            if (int.TryParse(strInput, out var ret))
            {
                return ret;
            }

            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// This method converts the input to a long. If the input is null or empty, it returns 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToInt64(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();
            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            if (long.TryParse(strInput, out var ret))
            {
                return ret;
            }

            try
            {
                ret = Convert.ToInt64(strInput);
                return ret;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// This method converts the string input to a boolean. If the input is null or empty, it returns false.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ToBool(this string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input?.ToLower() == "true" || input?.ToLower().Trim() == "1";
        }

        /// <summary>
        /// Converts an object to a boolean value.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>True if the object is not null and can be converted to a boolean value, false otherwise.</returns>
        public static bool ToBool(this object? input) => input != null && ToBool(input.ToString());

        /// <summary>
        /// Converts an object to a DateTime value.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The converted DateTime value. If the input is null, returns DateTime.MinValue.</returns>
        public static DateTime ToDateTime(this object? input)
        {
            if (input == null)
            {
                return DateTime.MinValue;
            }

            DateTime.TryParse(input.ToString(), out var ret);
            return ret;
        }

        /// <summary>
        /// Converts an object to a 16-bit signed integer.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The converted 16-bit signed integer.</returns>
        public static short ToInt16(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();
            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            if (short.TryParse(strInput, out var ret))
            {
                return ret;
            }

            try
            {
                ret = Convert.ToInt16(strInput);
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts an object to a byte value.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The byte value of the object. Returns 0 if the object is null or empty, or if the conversion fails.</returns>
        public static byte ToByte(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();
            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            if (byte.TryParse(strInput, out var ret))
            {
                return ret;
            }

            try
            {
                ret = Convert.ToByte(strInput);
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts an object to a float value.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The float value of the object, or 0 if the conversion fails.</returns>
        public static float ToFloat(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();
            if (string.IsNullOrEmpty(strInput))
            {
                return 0;
            }

            if (float.TryParse(strInput, out var ret))
                return ret;

            try
            {
                ret = Convert.ToSingle(strInput);
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converts an object to a Guid.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The Guid representation of the object, or Guid.Empty if the conversion fails.</returns>
        public static Guid ToGuid(this object? input)
        {
            var strInput = input == null ? "" : input.ToString();
            if (string.IsNullOrEmpty(strInput))
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(strInput, out var ret))
            {
                return ret;
            }

            try
            {
                ret = new Guid(strInput);
                return ret;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}