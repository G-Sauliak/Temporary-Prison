using AutoMapper;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
            TempData["RelationshipAddPrisoner"] = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
            ViewBag.RelationshipStatus = TempData["RelationshipAddPrisoner"];
            ViewBag.RedirectUrl = Url.Action("ListOfPrisoners", "Prisoner");
            return View(new PrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public ActionResult AddPrisoner(PrisonerViewModel model, string RedirectUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = TempData["RelationshipAddPrisoner"];
                return View(model);
            }

            var photo = Request.Files[0];

            if (photo != null && PhotosExtensions.SupportedFormat(photo) && PhotosExtensions.CheckSize(photo))
            {
                var fileName = Path.GetFileName(photo.FileName);
                var photoSavePath = Path.Combine(Server.MapPath("~/Content/PhotosOfPrisoners"), fileName);

                Image.FromStream(photo.InputStream).SaveToFolder(photoSavePath);

                model.Photo = $"~/Content/PhotosOfPrisoners/{fileName}";

                var prisoner = Mapper.Map<PrisonerViewModel, Prisoner>(model);

                int newID;
                prisonerProvider.AddPrisoner(prisoner, out newID);

                //TODO

                return RedirectToAction("ListOfPrisoners", "Prisoner");
            }

            ModelState.AddModelError(string.Empty, "Incorrect file");
            ViewBag.RelationshipStatus = TempData["RelationshipAddPrisoner"];

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public ActionResult EditPrisoner(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);

            if (prisoner != null)
            {
                TempData["RelationshipEditUser"] = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                var model = Mapper.Map<Prisoner, PrisonerViewModel>(prisoner);
                ViewBag.RelationshipStatus = TempData["RelationshipEditUser"];
                return View(model);
            }

            return Redirect(Url.Action("ListOfPrisoners", "Prisoner"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public ActionResult EditPrisoner(PrisonerViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = TempData["RelationshipEditUser"];
                return View(model);
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