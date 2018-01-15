using System;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Business.Extensions;
using Temporary_Prison.Business.SiteConfigService;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Hosting;
using Temporary_Prison.Data.Services;

namespace Temporary_Prison.Business.PrisonManager
{
    public class PrisonManager : IPrisonManager
    {
        private readonly IConfigService siteConfigService;
        private readonly IPrisonerDataService prisonerDataService;

        public PrisonManager() : this(new ConfigService(), new PrisonerDataService())
        { }

        public PrisonManager(IConfigService siteConfigService, IPrisonerDataService prisonerDataService)
        {
            this.prisonerDataService = prisonerDataService;
            this.siteConfigService = siteConfigService;
        }
        public void AddPrisoner(Prisoner prisoner, HttpPostedFileBase postImage, out int newId)
        {
            prisoner.Photo = default(string);

            if (postImage != null
                && ImageHelper.IsSupportedFormat(postImage,siteConfigService.AllowedPhotoTypes)
                && ImageHelper.IsValidSize(postImage,siteConfigService.MaxPhotoSize))
            {
                var photo = Image.FromStream(postImage.InputStream);

                var photoExtensions = Path.GetExtension(postImage.FileName);
                var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);

                var savePath = HostingEnvironment
                    .MapPath($"~/{siteConfigService.ContentPath}/{siteConfigService.PrisonerPhotoPath}");
                var photoSavePath = Path.Combine(savePath, photoName);

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                photo.ResizeMinimal(new Size(siteConfigService.PhotoWidth, siteConfigService.PhotoHeight))
                     .Save(photoSavePath);

                prisoner.Photo = photoName;
            }

            prisonerDataService.AddPrisoner(prisoner, out newId);
        }

        public void DeleteDetention(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePrisoner(int id)
        {
            throw new NotImplementedException();
        }

        public void EditDetention(Detention detention)
        {
            throw new NotImplementedException();
        }

        public void EditPrisoner(Prisoner prisoner)
        {
            throw new NotImplementedException();
        }


        public void RegisterDetention(RegistDetention registDetention)
        {
            throw new NotImplementedException();
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisoner release)
        {
            throw new NotImplementedException();
        }
    }
}
