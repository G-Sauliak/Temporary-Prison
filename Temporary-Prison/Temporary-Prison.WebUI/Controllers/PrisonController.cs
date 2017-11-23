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
            return View(prisonProvider.GetPrisoner());
        }

        public ActionResult DetailsPrisoner(int? id)
        {
        
            if (!id.HasValue)
            {
                RedirectToAction("ListPrisoner");
            }

            PrisonerProfile prisoner = prisonProvider.GetPrisoner().FirstOrDefault(p => p.UserId == id);

            return View(prisoner);
        }

        


    }
}