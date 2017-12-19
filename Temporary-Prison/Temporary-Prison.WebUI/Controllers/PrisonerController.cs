using log4net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    public class PrisonerController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerController(IPrisonerProvider prisonerProvider)
        {
            this.prisonerProvider = prisonerProvider;
        }

        // GET: Prisoner/ListOfPrisoners
        [HttpGet]
        [Authorize]
        public ActionResult ListOfPrisoners(int? page, string sort, string currentFilter, string search, int? currentTotal)
        {
            const int pageSize = 4;
            var totalCount = default(int);
            var _currentTotal = currentTotal ?? default(int);

            ViewBag.CurrentSort = sort;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            if (_currentTotal != default(int))
            {
                totalCount = _currentTotal;
            }
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            var pageNum = page ?? 1;
            int skip = (pageNum - 1) * pageSize;

            var listPrisoners = prisonerProvider.GetPrisonerForPagedList(skip, pageSize, ref totalCount);
            ViewBag.TotalCountPrisoners = totalCount;

            var prisonersPagedList = new StaticPagedList<Prisoner>(listPrisoners, pageNum, pageSize, totalCount);

            return View(prisonersPagedList);
        }

        [Authorize]
        public async Task<ActionResult> FindPrisonersByName(string search)
        {
            var listPrisoners = await prisonerProvider.FindPrisonersByName(search);

            var prisonersPagedList = new StaticPagedList<Prisoner>(
                listPrisoners,
                1, listPrisoners.Count,
                listPrisoners.Count
                );

            ViewBag.TotalCountPrisoners = prisonersPagedList.Count;

            return PartialView("PrisonerPanel", prisonersPagedList);
        }
        [HttpGet]
        [Authorize]
        public ActionResult DetailsOfPrisoner(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("ListPrisoner");
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);

            return View(prisoner);
        }

    }
}