using System;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Business.Extensions;
using System.Text.RegularExpressions;
using Temporary_Prison.Business.SiteConfigService;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.Hosting;

namespace Temporary_Prison.Business.PrisonManager
{
    public class PrisonManager : IPrisonManager
    {
        private readonly IConfigService siteConfigService;
        public PrisonManager() : this(new ConfigService())
        {

        }

        public PrisonManager(IConfigService siteConfigService)
        {
            this.siteConfigService = siteConfigService;
        }
        public void AddPrisoner(Prisoner prisoner, out int newId)
        {
            throw new NotImplementedException();
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

        public bool TryUploadPhoto(HttpPostedFileBase postImage, out string savedPath)
        {
            var allowedPhotoTypes = Regex.Split(siteConfigService.AllowedPhotoTypes, ";");

            if (allowedPhotoTypes.Contains(postImage.ContentType))
            {
                if (postImage.ContentLength <= siteConfigService.MaxPhotoSize * 1024)
                {
                    var photo = Image.FromStream(postImage.InputStream);

                    var photoExtensions = Path.GetExtension(postImage.FileName);
                    var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);

                    var savePath = HostingEnvironment.MapPath(siteConfigService.PrisonerPhotoPath);
                    var photoSavePath = Path.Combine(savePath, photoName);

                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    photo.Save(photoSavePath);
                    //var s = VirtualPathUtility.GetDirectory(photoSavePath);

                    //   photo.SaveToFolder(photoSavePath);

                    savedPath = savePath;

                    return true;
                }
            }
            savedPath = default(string);
            return false;
        }
    }
}
