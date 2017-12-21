using System.Collections.Generic;
using System.Web;

namespace Temporary_Prison.Extensions
{
    public static class PhotosExtensions
    {
        private const int maxSize = 1024;

        private static readonly IReadOnlyList<string> imageTypes = new List<string>
                                   {
                                       "image/jpg",
                                       "image/jpeg",
                                       "image/png"
                                   };


        public static bool SupportedFormat(HttpPostedFileBase file)
        {
            foreach (var format in imageTypes)
            {
                if (file.ContentType == format)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckSize(HttpPostedFileBase file)
        {
            if (file.ContentLength <= maxSize * 1024)
            {
                return true;
            }
            return false;
        }

    }

}