using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Temporary_Prison.Business.Extensions
{
    public static class ImageHelper
    {

        public static void SaveToFolder(this Image image, string ImageSavePath)
        {
            if (image != null)
            {
                var directoryName =  Path.GetDirectoryName(ImageSavePath);

                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                image.Save(ImageSavePath);
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

        public static Image ResizeProportional(this Image image, Size maxSize)
        {
            // if the input image width or height are already less than the required dimensions
            if ((maxSize.Height == 0 || maxSize.Height >= image.Height)
                && (maxSize.Width == 0 || maxSize.Width >= image.Width))
            {
                return image;
            }

            else
            {
                return new Bitmap(image, maxSize) as Image;
            }
        }
    }
}
