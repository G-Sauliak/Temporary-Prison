using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Temporary_Prison.Extensions;
using Temporary_Prison.WebUI.SiteConfigService;

namespace Temporary_Prison.Services.FileService
{
    public class PrisonFileService : IFileService
    {
        private readonly IConfigService siteConfigService;

        public PrisonFileService(IConfigService siteConfig)
        {
            this.siteConfigService = siteConfig;
        }

        public void RemoveFile(string fileName)
        {
            var deletePhotoPath = Path
                      .Combine(HostingEnvironment
                      .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.PhotoPath}"), fileName);

            var deleteAvatarPath = Path
                .Combine(HostingEnvironment
                .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.AvatarPath}"), fileName);

            if (File.Exists(deletePhotoPath) && File.Exists(deleteAvatarPath))
            {
                File.Delete(deletePhotoPath);
                File.Delete(deleteAvatarPath);
            }
        }

        public bool UploadFile(HttpPostedFileBase postImage, string photoName)
        {
            var prisonerPhoto = Image.FromStream(postImage.InputStream);

            if (ImageHelper.IsSupportedFormat(postImage.ContentType, siteConfigService.AllowedPhotoTypes)
                && ImageHelper.CheckSize(postImage.ContentLength, siteConfigService.ImageMaxSize))
            {

                var avatarSavePath = Path
                    .Combine(HostingEnvironment
                    .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.AvatarPath}"), photoName);

                var photoSavePath = Path
                    .Combine(HostingEnvironment
                    .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.PhotoPath}"), photoName);

                prisonerPhoto.ResizeProportional(new Size(siteConfigService.PhotoWidth, siteConfigService.PhotoHeight))
                   .SaveToFolder(photoSavePath);

                prisonerPhoto.ResizeMinimalSqueeze(new Size(siteConfigService.AvatarWidth, siteConfigService.AvatarHeight))
                     .SaveToFolder(avatarSavePath);

                return true;
            }
            return false;
        }
    }
}