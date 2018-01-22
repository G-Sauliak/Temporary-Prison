using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Temporary_Prison.Extensions
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

        public static bool IsSupportedFormat(string type, string allowedTypes)
        {
            var allowedPhotoTypes = Regex.Split(allowedTypes, ";");

            return allowedPhotoTypes.Contains(type);
        }

        public static bool CheckSize(long size, int maxSize)
        {
            if (size <= maxSize * 1024)
            {
                return true;
            }
            return false;
        }

        public static Image ResizeProportional(this Image image, Size maxSize)
        {
            var imageResize = new Size();

            // if image dimensions are less than max no resize required
            if ((maxSize.Width == 0 || maxSize.Height >= image.Height)
                && (maxSize.Width == 0 || maxSize.Width >= image.Width))
            {
                return image;
            }

            else
            {
                if (maxSize.Height == 0 && maxSize.Width > 0)
                {
                    imageResize.Width = maxSize.Width;
                    imageResize.Height = image.Height * maxSize.Width / image.Width;
                }

                if (maxSize.Height > 0 && maxSize.Width == 0)
                {
                    imageResize.Height = maxSize.Height;
                    imageResize.Width = image.Width * maxSize.Height / image.Height;
                }

                if (maxSize.Height > 0 && maxSize.Width > 0)
                {
                    imageResize.Width = maxSize.Width;
                    imageResize.Height = image.Height * maxSize.Width / image.Width;

                    if (imageResize.Height > maxSize.Height)
                    {
                        imageResize.Height = maxSize.Height;
                        maxSize.Width = image.Width * maxSize.Height / image.Height;
                    }
                }

                return new Bitmap(image, imageResize);
            }
        }

        public static Image ResizeMinimalSqueeze(this Image image, Size maxSize)
        {
            int cropWidth = image.Width, cropHeight = image.Height; 

            int xCrop = default(int), yCrop = default(int); 
            double widthRatio = default(int); 
            double heightRatio = default(int); 

            widthRatio = (double)image.Width / maxSize.Width;
            heightRatio = (double)image.Height / maxSize.Height;

            if (widthRatio > heightRatio)
            {
                cropWidth = (int)(maxSize.Width * heightRatio);
                xCrop = (image.Width - cropWidth) / 2;
            }
            else
            {
                cropHeight = (int)(maxSize.Height * widthRatio);
                yCrop = (image.Height - cropHeight) / 2;
            }

            var bmpImage = new Bitmap(image);
            var bmpCrop = bmpImage.Clone(new Rectangle(xCrop, yCrop, cropWidth, cropHeight), bmpImage.PixelFormat);

            return new Bitmap(bmpCrop, maxSize);
        }
    }
}
