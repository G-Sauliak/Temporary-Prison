using log4net;
using System.Linq;
using System.Web.Mvc;
using Temporary_Prison.Business;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    public class PrisonerController : Controller
    {
        private readonly IPrisonerProvider prisonProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerController(IPrisonerProvider prisonProvider)
        {
            this.prisonProvider = prisonProvider;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ListOfPrisoners(int? page, string sort, string currentFilter, string search)
        {
            var listPrisoners = prisonProvider.GetPrisoner();

            const int pageSize = 4;

            ViewBag.CurrentSort = sort;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.CountPrisoners = listPrisoners.Count();

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            int pageNum = page ?? 1;

            return View(listPrisoners.ToPagedList(pageNum,pageSize));
        }

        public ActionResult DetailsOfPrisoner(int? id)
        {

            if (!id.HasValue)
            {
                RedirectToAction("ListPrisoner");
            }

            var prisoner = prisonProvider.GetPrisonerById(id.Value);

            return View(prisoner);
        }

    }
}