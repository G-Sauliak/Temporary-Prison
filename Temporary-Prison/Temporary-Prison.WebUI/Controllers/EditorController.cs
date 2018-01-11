using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Dependencies.MapperRegistry;
using Temporary_Prison.Enums;
using Temporary_Prison.Extensions;
using Temporary_Prison.MapperProfile;
using Temporary_Prison.Models;
using Temporary_Prison.SiteConfigService;

namespace Temporary_Prison.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class EditorController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;
        private readonly IConfigService siteConfigService;
        public EditorController() : this(new PrisonerProvider(), new ConfigService())
        {
            MapperProfiles.Configuration.AddProfile(new WebMapper());
            MapperProfiles.InitialiseMappers();
        }

        public EditorController(IPrisonerProvider prisonerProvider, IConfigService siteConfigService)
        {
            this.siteConfigService = siteConfigService;
            this.prisonerProvider = prisonerProvider;
        }

        // GET: Editor/AddPrisoner
        [HttpGet]
        public ActionResult AddPrisoner()
        {
            ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
            ViewBag.RedirectUrl = Url.Action("ListOfPrisoners", "Prisoner");
            return View(new CreateOrUpdatePrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrisoner(HttpPostedFileBase photo, CreateOrUpdatePrisonerViewModel model, string RedirectUrl)
        {
            if (!ModelState.IsValid)
            {
                if (model.PhoneNumbers.Count < 1) model.PhoneNumbers = new string[] { string.Empty };
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>(); ;
                return View(model);
            }

            if (photo != null
                && PhotosExtensions.SupportedFormat(photo, siteConfigService.AllowedPhotoTypes)
                && PhotosExtensions.CheckSize(photo, siteConfigService.MaxPhotoSize))
            {
                var photoExtensions = Path.GetExtension(photo.FileName);
                var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);
                var savePath = Server.MapPath($"~/Content/{siteConfigService.PrisonerPhotoPath}");
                var photoSavePath = Path.Combine(savePath, photoName);

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                photo.SaveAs(photoSavePath);

                model.Photo = $"/Content/{siteConfigService.PrisonerPhotoPath}/{photoName}";

                var prisoner = Mapper.Map<CreateOrUpdatePrisonerViewModel, Prisoner>(model);

                var newID = default(int);
                prisonerProvider.AddPrisoner(prisoner, out newID);

                return RedirectToAction("CreateDetention", "Editor", new { prisonerId = newID });
            }
            else if (photo == null)
            {
                var prisoner = Mapper.Map<CreateOrUpdatePrisonerViewModel, Prisoner>(model);

                prisoner.Photo = $"/Content/{siteConfigService.defaultPhotoOfPrisonerPath}";

                var newID = default(int);
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
                var model = Mapper.Map<Prisoner, CreateOrUpdatePrisonerViewModel>(prisoner);
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }
            return HttpNotFound();
        }

        //POST: Editor/EditPriosner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrisoner(CreateOrUpdatePrisonerViewModel model, HttpPostedFileBase photo, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            if (photo != null
                && PhotosExtensions.SupportedFormat(photo, siteConfigService.AllowedPhotoTypes)
                && PhotosExtensions.CheckSize(photo, siteConfigService.MaxPhotoSize))
            {
                //TODO
            }
            var prisoner = Mapper.Map<CreateOrUpdatePrisonerViewModel, Prisoner>(model);
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
            var prisoner = prisonerProvider.GetPrisonerById(prisonerId.Value);

            if (prisoner != null)
            {
                ViewBag.PrisonerFullName = $"{prisoner.FirstName} {prisoner.LastName}";
                var model = new RegistrationOfDetentionViewModel
                {
                    PrisonerID = prisonerId.Value
                };

                return View(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpGet]
        //GET : Editor/EditDetention
        public ActionResult EditDetention(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detention = prisonerProvider.GetDetentionById(id.Value);

            if (detention == null)
            {
                return HttpNotFound();
            }

            ViewBag.ReturnUrl = Url.Action("DetailsOfDetention", "Prisoner", new { id = id.Value });

            if (detention.DateOfRelease != null)
            {
                var fullDetentionModel = Mapper.Map<Detention, EditFullDetentionViewModel>(detention);
                return View("EditFullDetention", fullDetentionModel);
            }
            var DetentionModel = Mapper.Map<Detention, EditDetentionViewModel>(detention);

            return View(DetentionModel);
        }

        //GET : Editor/EditDetention
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetention(EditDetentionViewModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var detention = Mapper.Map<EditDetentionViewModel, Detention>(model);
            prisonerProvider.EditDetention(detention);
            return RedirectToLocal(ReturnUrl);
        }

        //GET : Editor/EditFullDetention
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFullDetention(EditFullDetentionViewModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var detention = Mapper.Map<EditFullDetentionViewModel, Detention>(model);
            prisonerProvider.EditDetention(detention);
            return RedirectToLocal(ReturnUrl);
        }

        //POST : Editor/CreateListOfDetention
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
        //GET : Editor/ReleaseOfPriosner
        [HttpGet]
        public ActionResult ReleaseOfPrisoner(int? detentionID)
        {
            if (!detentionID.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detention = prisonerProvider.GetDetentionById(detentionID.Value);

            if (detention == null)
            {
                return HttpNotFound();
            }
            var model = new ReleaseOfPrisonerViewModel()
            {
                DetentionID = detentionID.Value
            };
            return View(model);
        }
        //POST : Editor/ReleaseOfPriosner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReleaseOfPrisoner(ReleaseOfPrisonerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var detention = prisonerProvider.GetDetentionById(model.DetentionID);
                if (detention != null && detention.DateOfRelease == null)
                {
                    var releaseOfPriosner = Mapper.Map<ReleaseOfPrisonerViewModel, ReleaseOfPrisoner>(model);

                    prisonerProvider.ReleaseOfPrisoner(releaseOfPriosner);
                    return RedirectToAction("DetailsOfPrisoner", "Prisoner", new { id = detention.PrisonerID });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteDetention(int? id, int? prisonerId, int? page, int? totalCount)
        {
            if (id.HasValue && prisonerId.HasValue)
            {
                prisonerProvider.DeleteDetention(id.Value);

                return RedirectToAction("DetailsOfPrisoner", "Prisoner",
                    new
                    {
                        id = prisonerId.Value,
                        page = page,
                        totalCount = totalCount
                    });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private ActionResult RedirectToLocal(string redirectUrl)
        {
            if (Url.IsLocalUrl(redirectUrl))
            {
                return Redirect(redirectUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}