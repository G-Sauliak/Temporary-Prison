using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Enums;
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
           
           if (!ModelState.IsValid || Request.Files[0] == null)
            {
                ModelState.AddModelError(string.Empty, "The prisoner cannot be added");
                ViewBag.RelationshipStatus = Enum.GetValues(typeof(RelationshipStatus)).Cast<RelationshipStatus>();
                return View(model);
            }

            var file = Request.Files[0];

            var fileName = Path.GetFileName(file.FileName);

            var path = Path.Combine(Server.MapPath("~/Content/PhotosOfPrisoners"), fileName);

            file.SaveAs(path);

            model.Photo = $"/Content/PhotosOfPrisoners/{fileName}";

            var prisoner = Mapper.Map<AddPrisonerViewModel, Prisoner>(model);

            int newID;

            if (prisonerProvider.TryAddPrisoner(prisoner,out newID))
            {
                //TODO
            }
            return Redirect(RedirectUrl);
            //TODO
        }
    }
}