using System.Linq;
using System.Web.Mvc;
using Temporary_Prison.Business;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Controllers
{
    public class PrisonController : Controller
    {
        private readonly IPrisonProvider prisonProvider;

        public PrisonController(IPrisonProvider prisonProvider)
        {
            this.prisonProvider = prisonProvider;
        }
        // GET: Prison
        public ActionResult ListOfPrisoners()
        {

            var listPrisoners = prisonProvider.GetPrisoner();

            return View(listPrisoners);
        }

        public ActionResult DetailsPrisoner(int? id)
        {

            if (!id.HasValue)
            {
                RedirectToAction("ListPrisoner");
            }

            Prisoner prisoner = prisonProvider.GetPrisoner().FirstOrDefault(p => p.PrisonerId == id);

            return View(prisoner);
        }




    }
}