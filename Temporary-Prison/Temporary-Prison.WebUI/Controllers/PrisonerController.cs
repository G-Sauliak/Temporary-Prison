using System.Linq;
using System.Web.Mvc;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Controllers
{
    public class PrisonerController : BaseController
    {
        public ActionResult ListOfPrisoners()
        {
            var listPrisoners = prisonProvider.GetPrisoner();

            return View(listPrisoners);
        }

        public ActionResult DetailsOfPrisoner(int? id)
        {

            if (!id.HasValue)
            {
                RedirectToAction("ListPrisoner");
            }

            var prisoner = prisonProvider.GetPrisoner().FirstOrDefault(p => p.PrisonerId == id);

            return View(prisoner);
        }
    }
}