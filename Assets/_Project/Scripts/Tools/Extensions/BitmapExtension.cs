using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SharpStackConvert
{
    public static class BitmapExtension
    {
        /// <summary>
        /// Converts a System.Drawing.Bitmap image to a base64 string.
        /// </summary>
        /// <param name="bitmap">The bitmap image to convert.</param>
        /// <returns>The base64 string representation of the image.</returns>
        public static string ImageToBase64(this Bitmap bitmap)
        {
            if (bitmap == null) 
                return "";
            
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            var byteImage = ms.ToArray();
            return byteImage.ToBase64();
        }
    }
}
