using AutoMapper;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Enums;
using Temporary_Prison.Extensions;
using Temporary_Prison.Models;

namespace Temporary_Prison.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class EditorController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;

        public EditorController(IPrisonerProvider prisonerProvider)
        {
            this.prisonerProvider = prisonerProvider;
        }
        // GET: Editor/AddPrisoner
        [HttpGet]
        public ActionResult AddPrisoner()
        {
            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
            ViewBag.RedirectUrl = Url.Action("ListOfPrisoners", "Prisoner");
            return View(new PrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrisoner(HttpPostedFileBase photo, PrisonerViewModel model, string RedirectUrl)
        {
            if (!ModelState.IsValid)
            {
                if (model.PhoneNumbers.Count < 1) model.PhoneNumbers = new string[] { "" };
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>(); ;
                return View(model);
            }

           // var photo = Request.Files[0];

            if (photo != null && PhotosExtensions.SupportedFormat(photo) && PhotosExtensions.CheckSize(photo))
            {
                var photoExtensions = Path.GetExtension(photo.FileName);
                var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);
                var savePath = Server.MapPath("~/Content/PhotosOfPrisoners");
                var photoSavePath = Path.Combine(savePath, photoName);

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                photo.SaveAs(photoSavePath);

                model.Photo = $"/Content/PhotosOfPrisoners/{photoName}";

                var prisoner = Mapper.Map<PrisonerViewModel, Prisoner>(model);

                int newID;

                prisonerProvider.AddPrisoner(prisoner, out newID);

                TempData["NewPrisoner"] = prisoner;
                TempData["newID"] = newID;

                //TODO

                return RedirectToAction("ListOfPrisoners", "Prisoner");
            }

            ModelState.AddModelError(string.Empty, "Incorrect file");
            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();

            return View(model);
        }
        [HttpGet]
        public ActionResult EditPrisoner(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);

            if (prisoner != null)
            {
                TempData["photo"] = prisoner.Photo;
                var model = Mapper.Map<Prisoner, PrisonerViewModel>(prisoner);
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            return Redirect(Url.Action("ListOfPrisoners", "Prisoner"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrisoner(PrisonerViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            var photo = Request.Files[0];

            if (photo != null && PhotosExtensions.SupportedFormat(photo) && PhotosExtensions.CheckSize(photo))
            {

                if (!TempData["photo"].Equals(photo.FileName))
                {
                }

            }

            var prisoner = Mapper.Map<PrisonerViewModel, Prisoner>(model);
            prisonerProvider.EditPrisoner(prisoner);

            return RedirectToAction("ListOfPrisoners", "Prisoner");
        }

        public ActionResult DeletePrisoner(int? id)
        {
            if (id.HasValue)
            {
                prisonerProvider.DeletePrisoner(id.Value);
            }

            return RedirectToAction("ListOfPrisoners", "Prisoner");
        }

    }
}