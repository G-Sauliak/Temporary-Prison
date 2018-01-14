using System.Drawing;
using System.IO;


namespace Temporary_Prison.Business.Extensions
{
    public static class ImageHelperExtension
    {

        public static void SaveToFolder(this Image image, string savePath)
        {
            if (image != null)
            {
                image.Save(savePath);
            }
        }

    }
}
