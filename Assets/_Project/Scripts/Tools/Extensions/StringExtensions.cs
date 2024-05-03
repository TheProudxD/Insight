using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public static class StringExtensions
    {
        public static bool TryBool(this string self) => bool.TryParse(self, out var res) && res;

        public static float TrySingle(this string self) => float.TryParse(self, out var res) ? res : 0f;

        /// <summary>
        /// Returns 0-255
        /// </summary>
        public static int Hex_to_Dec(this string hex) => Convert.ToInt32(hex, 16);

        /// <summary>
        /// Returns a float between 0->1
        /// </summary>
        public static float Hex_to_Dec01(string hex) => hex.Hex_to_Dec() / 255f;

        /// <summary>
        /// Converts a string to its MD5 hash representation.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The MD5 hash representation of the input string.</returns>
        public static string ToMD5(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var md5 = MD5.Create();
            var hashedBytes = md5.ComputeHash(encoding.GetBytes(input));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA1 hash value using the specified encoding.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The SHA1 hash value of the input string.</returns>
        public static string ToSHA1(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            using var sha1 = SHA1.Create();
            var hashedBytes = sha1.ComputeHash(encoding.GetBytes(input));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA256 hash value.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The SHA256 hash value of the input string.</returns>
        public static string ToSHA256(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(encoding.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA384 hash value using the specified encoding.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The SHA384 hash value of the input string.</returns>
        public static string ToSHA384(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha384 = SHA384.Create();
            var hashedBytes = sha384.ComputeHash(encoding.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA512 hash representation.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The SHA512 hash representation of the input string.</returns>
        public static string ToSHA512(this string input, Encoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha512 = SHA512.Create();
            var hashedBytes = sha512.ComputeHash(encoding.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its MD5 hash value.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>The MD5 hash value of the input string.</returns>
        public static string ToMD5(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            using var md5 = MD5.Create();
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA1 hash value.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>The SHA1 hash value of the input string.</returns>
        public static string ToSHA1(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha1 = SHA1.Create();
            var hashedBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA256 hash value.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The SHA256 hash value of the input string.</returns>
        public static string ToSHA256(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA384 hash value.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>The SHA384 hash value of the input string.</returns>
        public static string ToSHA384(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha384 = SHA384.Create();
            var hashedBytes = sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a string to its SHA512 hash value.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The SHA512 hash value of the input string.</returns>
        public static string ToSHA512(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            using var sha512 = SHA512.Create();
            var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Converts a base64 encoded string to a byte array.
        /// </summary>
        /// <param name="input">The base64 encoded string to convert.</param>
        /// <returns>The byte array representation of the base64 encoded string.</returns>
        public static byte[] FromBase64ToByteArray(this string input) =>
            string.IsNullOrEmpty(input) ? Array.Empty<byte>() : Convert.FromBase64String(input);


        /// <summary>
        /// Converts a base64 encoded string to a byte array using the specified encoding.
        /// </summary>
        /// <param name="input">The base64 encoded string to convert.</param>
        /// <param name="encoding">The encoding to use for the conversion.</param>
        /// <returns>A byte array representing the decoded base64 string.</returns>
        public static byte[] FromBase64ToByteArray(this string input, Encoding encoding) =>
            string.IsNullOrEmpty(input) ? Array.Empty<byte>() : Convert.FromBase64String(input);

        /// <summary>
        /// Converts a base64 encoded string to its original UTF8 string representation.
        /// </summary>
        /// <param name="input">The base64 encoded string to convert.</param>
        /// <returns>The original UTF8 string representation of the base64 encoded string.</returns>
        public static string FromBase64(this string input) => string.IsNullOrEmpty(input)
            ? ""
            : Encoding.UTF8.GetString(Convert.FromBase64String(input));

        /// <summary>
        /// Converts a base64 encoded string to a Bitmap object.
        /// </summary>
        /// <param name="input">The base64 encoded string.</param>
        /// <returns>The Bitmap object.</returns>
        public static Bitmap FromBase64ToBitmap(this string input)
        {
            var byteImage = FromBase64ToByteArray(input);
            if (byteImage == null)
                return null;
            var ms = new System.IO.MemoryStream(byteImage);
            var bitmap = new Bitmap(ms);
            return bitmap;
        }

        /// <summary>
        /// Converts a string to its Base64 representation using the specified encoding.
        /// </summary>
        /// <param name="input">The string to be converted.</param>
        /// <param name="encoding">The encoding to be used for converting the string to bytes.</param>
        /// <returns>The Base64 representation of the input string.</returns>
        public static string ToBase64(this string input, Encoding encoding) => string.IsNullOrEmpty(input)
            ? ""
            : Convert.ToBase64String(encoding.GetBytes(input));

        /// <summary>
        /// Converts a base64 encoded string to its original form using the specified encoding.
        /// </summary>
        /// <param name="input">The base64 encoded string to convert.</param>
        /// <param name="encoding">The encoding to use for the conversion.</param>
        /// <returns>The original string represented by the base64 encoded input.</returns>
        public static string FromBase64(this string input, Encoding encoding) => string.IsNullOrEmpty(input)
            ? ""
            : encoding.GetString(Convert.FromBase64String(input));

        /// <summary>
        /// Converts a string to its Base64 representation.
        /// </summary>
        /// <param name="input">The input string to be converted.</param>
        /// <returns>The Base64 representation of the input string.</returns>
        public static string ToBase64(this string input) => string.IsNullOrEmpty(input)
            ? ""
            : Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
    }
}