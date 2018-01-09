using System.Text.RegularExpressions;
using System.Web;

namespace Temporary_Prison.Extensions
{
    public static class PhotosExtensions
    {
        public static bool SupportedFormat(HttpPostedFileBase photo, string allowedFormats)
        {
            foreach (var format in Regex.Split(allowedFormats, ";"))
            {
                if (photo.ContentType == format)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckSize(HttpPostedFileBase photo, int maxSize)
        {
            if (photo.ContentLength <= maxSize * 1024)
            {
                return true;
            }
            return false;
        }

    }

}