using AutoMapper;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.WebMapperProfile;
using Temporary_Prison.Models;
using Temporary_Prison.Business.PrisonManager;
using log4net;
using Temporary_Prison.Enums;
using System.IO;
using Temporary_Prison.WebUI.SiteConfigService;
using Temporary_Prison.Services.FileService;

namespace Temporary_Prison.Controllers
{
    [Authorize(Roles = "Admin, Editor")]
    public class EditorController : Controller
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IPrisonerProvider prisonerProvider;
        private readonly IPrisonManager prisonManager;
        private readonly IConfigService siteConfigService;
        private readonly IFileService uploader;

        public EditorController() :
            this(new PrisonerProvider(),
            new PrisonManager(), 
            new ConfigService(), 
            new PrisonFileService(new ConfigService()
                ))
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new WebMapper());
            });
        }

        public EditorController(IPrisonerProvider prisonerProvider,
            IPrisonManager prisonManager,IConfigService siteConfigService, IFileService uploader)
        {
            this.prisonManager = prisonManager;
            this.prisonerProvider = prisonerProvider;
            this.siteConfigService = siteConfigService;
            this.uploader = uploader;
        }

        // GET: Editor/AddPrisoner
        [HttpGet]
        public ActionResult AddPrisoner()
        {

            ViewBag.RedirectUrl = Url.Action("ListOfPrisoners", "Prisoner");
            return View(new CreateOrUpdatePrisonerViewModel());
        }

        // POST: Editor/Addrisoner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPrisoner(HttpPostedFileBase postImage, CreateOrUpdatePrisonerViewModel model, string RedirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var prisoner = Mapper.Map<CreateOrUpdatePrisonerViewModel, Prisoner>(model);

            try
            {
                if (postImage != null && postImage.ContentLength > 0)
                {
                    var photoExtensions = Path.GetExtension(postImage.FileName);
                    var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);

                    if (uploader.UploadFile(postImage, photoName))
                    {
                        prisoner.Photo = photoName;
                    }

                    if (prisoner.BirthDate.Date > DateTime.Now.Date)
                }

                
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError(string.Empty, "incorrect photo format");
                log.Error(ae.Message);
                return View(model);
            }
            catch (CreateOrUpdatePrisonerException ce)
            {
                ModelState.AddModelError(string.Empty, ce.Message);
                log.Error(ae.Message);
                return View(model);
            }
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
                ViewBag.RelShipStatus = Enum.Parse(typeof(RelationshipStatus), prisoner.RelationshipStatus);
                return View(model);
            }
            return HttpNotFound();
        }

        //POST: Editor/EditPriosner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPrisoner(CreateOrUpdatePrisonerViewModel model, HttpPostedFileBase postImage, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var updatedPrisoner = Mapper.Map<CreateOrUpdatePrisonerViewModel, Prisoner>(model);
            var oldPrisoner = prisonerProvider.GetPrisonerById(model.PrisonerID);

            try
            {
                if (updatedPrisoner.Photo != oldPrisoner.Photo)
                {
                    uploader.RemoveFile(oldPrisoner.Photo);
                }

                if (postImage != null && postImage.ContentLength > 0)
                {
                    var photoExtensions = Path.GetExtension(postImage.FileName);
                    var photoName = string.Concat(DateTime.Now.Ticks, photoExtensions);

                    if (uploader.UploadFile(postImage, photoName))
                    {
                        updatedPrisoner.Photo = photoName;
                    }
                }
               
                prisonManager.EditPrisoner(updatedPrisoner);
            }
            catch (ArgumentException ae)
            {
                ModelState.AddModelError(string.Empty, "incorrect photo format");
                ViewBag.RelShipStatus = model.RelationshipStatus;
                log.Error(ae.Message);
                return View(model);
            }

            return RedirectToAction("ListOfPrisoners", "Prisoner");
        }

        public ActionResult DeletePrisoner(int? id)
        {
            if (id.HasValue)
            {
                var prisoner = prisonerProvider.GetPrisonerById(id.Value);
                if (prisoner != null)
                {
                    prisonManager.DeletePrisoner(id.Value);
                    if (prisoner.Photo != null)
                    {
                        uploader.RemoveFile(prisoner.Photo);
                    }
                }
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
            prisonManager.EditDetention(detention);
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
            prisonManager.EditDetention(detention);
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
                    prisonManager.RegisterDetention(regist);

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

                    prisonManager.ReleaseOfPrisoner(releaseOfPriosner);
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
                prisonManager.DeleteDetention(id.Value);

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