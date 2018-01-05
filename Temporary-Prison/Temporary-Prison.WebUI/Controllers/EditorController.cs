using AutoMapper;
using System;
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
    [Authorize(Roles = "Admin, Editor")]
    public class EditorController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;

        public EditorController() : this(new PrisonerProvider())
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Prisoner, CreatePrisonerViewModel>();
                cfg.CreateMap<User, EditUserViewModel>();
            });
        }

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
            return View(new CreatePrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrisoner(HttpPostedFileBase photo, CreatePrisonerViewModel model, string RedirectUrl)
        {
            if (!ModelState.IsValid)
            {
                if (model.PhoneNumbers.Count < 1) model.PhoneNumbers = new string[] { "" };
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>(); ;
                return View(model);
            }

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

                var prisoner = Mapper.Map<CreatePrisonerViewModel, Prisoner>(model);

                int newID;
                prisonerProvider.AddPrisoner(prisoner, out newID);

                return RedirectToAction("CreateDetention", "Editor", new { prisonerId = newID });
            }
            else if (photo == null)
            {
                int newID;
                var prisoner = Mapper.Map<CreatePrisonerViewModel, Prisoner>(model);

                //TODO add ConfigurationManager
                prisoner.Photo = "/Content/DefaultPhoto/defaultPhotoPrisoner.jpg";
                prisonerProvider.AddPrisoner(prisoner, out newID);
                return RedirectToAction("CreateDetention", "Editor", new { prisonerId = newID });
            }


            ModelState.AddModelError(string.Empty, "Incorrect file");
            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();

            return View(model);
        }

        //GET: Editor/EditPriosner
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
                var model = Mapper.Map<Prisoner, CreatePrisonerViewModel>(prisoner);
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            return Redirect(Url.Action("ListOfPrisoners", "Prisoner"));
        }

        //POST: Editor/EditPriosner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrisoner(CreatePrisonerViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            var photo = Request.Files[0];

            if (photo != null && PhotosExtensions.SupportedFormat(photo) && PhotosExtensions.CheckSize(photo))
            {
                //TODO
            }
            var prisoner = Mapper.Map<CreatePrisonerViewModel, Prisoner>(model);
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

        //GET : Editor/CreateListOfDetention
        [HttpGet]
        public ActionResult CreateDetention(int? prisonerId)
        {
            if (!prisonerId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new RegistrationOfDetentionViewModel
            {
                PrisonerID = prisonerId.Value
            };

            return View(model);
        }

        //HOST : Editor/CreateListOfDetention
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetention(RegistrationOfDetentionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prisoner = prisonerProvider.GetPrisonerById(model.PrisonerID);
                if (prisoner != null)
                {
                    var regist = Mapper.Map<RegistrationOfDetentionViewModel, RegistDetention>(model);
                    prisonerProvider.RegisterDetention(regist);

                    return RedirectToAction("DetailsOfPrisoner", "Prisoner", new { id = model.PrisonerID });
                }
            }
            return View(model);
        }

    }
}