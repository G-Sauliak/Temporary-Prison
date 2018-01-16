using System;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Business.Extensions;
using Temporary_Prison.Business.SiteConfigService;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Hosting;
using Temporary_Prison.Data.Services;
using Temporary_Prison.Business.Providers;

namespace Temporary_Prison.Business.PrisonManager
{
    public class PrisonManager : IPrisonManager
    {
        private readonly IConfigService siteConfigService;
        private readonly IPrisonerDataService prisonerDataService;
        private readonly IPrisonerProvider prisonerProvider;

        public PrisonManager() : this(new ConfigService(), new PrisonerDataService(), new PrisonerProvider())
        { }

        public PrisonManager(IConfigService siteConfigService, IPrisonerDataService prisonerDataService,
            IPrisonerProvider prisonerProvider)
        {
            this.prisonerDataService = prisonerDataService;
            this.siteConfigService = siteConfigService;
            this.prisonerProvider = prisonerProvider;
        }
        public void AddPrisoner(Prisoner prisoner, HttpPostedFileBase postImage, out int newId)
        {
            string fileName = default(string);
            if (postImage.ContentLength > 0 && postImage != null)
            {
                TryUploadImage(postImage, out fileName);
            }
            prisoner.Photo = fileName;
            prisonerDataService.AddPrisoner(prisoner, out newId);
        }

        public void DeleteDetention(int id)
        {
            prisonerDataService.DeleteDetention(id);

            var cacheKey = $"Detention_{id}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void DeletePrisoner(int id)
        {

            var cacheKey = $"prisoner_{id}";

            HttpRuntime.Cache.Remove(cacheKey);
            HttpRuntime.Cache.Remove("prisonersForPagelist");
        }

        public void EditDetention(Detention detention)
        {
            prisonerDataService.EditDetention(detention);

            var cacheKey = $"Detention_{detention.DetentionID}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void EditPrisoner(Prisoner updatedPrisoner, HttpPostedFileBase postImage)
        {
            var currentPrisoner = prisonerProvider.GetPrisonerById(updatedPrisoner.PrisonerId);
            var fileName = default(string);

            if (postImage != null && postImage.ContentLength > 0 && string.IsNullOrEmpty(updatedPrisoner.Photo))
            {
                if (TryUploadImage(postImage, out fileName))
                {
                    if (!string.IsNullOrEmpty(currentPrisoner.Photo))
                    {
                        var deletePhotoPath = Path
                            .Combine(HostingEnvironment
                            .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.PhotoPath}"), currentPrisoner.Photo);

                        var deleteAvatarPath = Path
                            .Combine(HostingEnvironment
                            .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.AvatarPath}"), currentPrisoner.Photo);

                        if (File.Exists(deletePhotoPath) && File.Exists(deleteAvatarPath))
                        {
                            File.Delete(deletePhotoPath);
                            File.Delete(deleteAvatarPath);
                        }
                    }
                }
            }

            updatedPrisoner.Photo = fileName;

            prisonerDataService.EditPrisoner(updatedPrisoner);

            var cacheKey = $"prisoner_{updatedPrisoner.PrisonerId}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void RegisterDetention(RegistDetention registDetention)
        {
            prisonerDataService.RegisterDetention(registDetention);
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisoner release)
        {
            prisonerDataService.ReleaseOfPrisoner(release);
        }

        private bool TryUploadImage(HttpPostedFileBase postImage, out string fileName)
        {
            if (postImage != null
               && ImageHelper.IsSupportedFormat(postImage, siteConfigService.AllowedPhotoTypes)
               && ImageHelper.IsValidSize(postImage, siteConfigService.MaxPhotoSize))
            {
                var photo = Image.FromStream(postImage.InputStream);

                var photoExtensions = Path.GetExtension(postImage.FileName);
                var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);

                var avatarSavePath = Path
                    .Combine(HostingEnvironment
                    .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.AvatarPath}"), photoName);

                var photoSavePath = Path
                    .Combine(HostingEnvironment
                    .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.PhotoPath}"), photoName);

                photo.ResizeProportional(new Size(siteConfigService.PhotoWidth, siteConfigService.PhotoHeight))
                   .SaveToFolder(photoSavePath);

                photo.ResizeProportional(new Size(siteConfigService.AvatarWidth, siteConfigService.AvatarHeight))
                     .SaveToFolder(avatarSavePath);

                fileName = photoName;
                return true;
            }
            else
            {
                fileName = default(string);
                return false;
            }
        }

    }
}
