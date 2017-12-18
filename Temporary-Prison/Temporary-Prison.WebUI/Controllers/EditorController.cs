using AutoMapper;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Enums;
using Temporary_Prison.Extensions;
using Temporary_Prison.Models;

namespace Temporary_Prison.Controllers
{
    public class EditorController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;

        public EditorController(IPrisonerProvider prisonerProvider)
        {
            this.prisonerProvider = prisonerProvider;
        }
        // GET: Editor/AddPrisoner
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public ActionResult AddPrisoner()
        {
            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
            ViewBag.RedirectUrl = Url.Action("ListOfPrisoners", "Prisoner");
            return View(new AddPrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public ActionResult AddPrisoner(AddPrisonerViewModel model, string RedirectUrl)
        {
            var photo = Request.Files[0];

            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            if (photo != null && PhotosExtensions.SupportedFormat(photo) && PhotosExtensions.CheckSize(photo))
            {
                var fileName = Path.GetFileName(photo.FileName);
                var photoSavePath = Path.Combine(Server.MapPath("~/Content/PhotosOfPrisoners"), fileName);

                Image.FromStream(photo.InputStream).SaveToFolder(photoSavePath);

                model.Photo = $"/Content/PhotosOfPrisoners/{fileName}";

                var prisoner = Mapper.Map<AddPrisonerViewModel, Prisoner>(model);

                int newID;
                prisonerProvider.AddPrisoner(prisoner, out newID);
                
                    //TODO
                
                return Redirect(RedirectUrl);
            }

            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();

            return View(model);

        }
    }
}