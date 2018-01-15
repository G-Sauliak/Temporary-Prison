using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Temporary_Prison.Business.Extensions
{
    public static class ImageHelper
    {

        public static void SaveToFolder(this Image image, string savePath)
        {
            if (image != null)
            {
                image.Save(savePath);
            }
        }

        public static bool IsSupportedFormat(HttpPostedFileBase file, string allowedTypes)
        {
            var allowedPhotoTypes = Regex.Split(allowedTypes, ";");

            return allowedPhotoTypes.Contains(file.ContentType);
        }

        public static bool IsValidSize(HttpPostedFileBase photo, int maxSize)
        {
            if (photo.ContentLength <= maxSize * 1024)
            {
                return true;
            }
            return false;
        }

        public static Image ResizeMinimal(this Image img, Size maxSize)
        {
            var bit = new Bitmap(maxSize.Width, maxSize.Height);
            var graphics = Graphics.FromImage(bit);
            graphics.DrawImage(img, 0, 0, maxSize.Width, maxSize.Height);
            graphics.Dispose();
            return bit;
        }

    }
}
