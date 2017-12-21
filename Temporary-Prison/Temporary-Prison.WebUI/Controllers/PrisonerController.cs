using log4net;
using System.Collections.Generic;
using System.Web.Mvc;
using Temporary_Prison.Business.CacheManager;
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
        public ActionResult ListOfPrisoners(int? page, int? currentTotal, string search)
        {
            const int pageSize = 4;
            var totalCount = default(int);
            var _currentTotal = currentTotal ?? default(int);
            var listPrisoners = default(IReadOnlyList<Prisoner>);

            var pageNum = page ?? 1;
            int skip = (pageNum - 1) * pageSize;

            if (_currentTotal != default(int))
            {
                totalCount = _currentTotal;
            }

            listPrisoners = prisonerProvider.
                GetPrisonerForPagedList(skip, pageSize, ref totalCount, search);

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                return View(listPrisoners.ToPagedList(pageNum, pageSize));
            }
            ViewBag.TotalCountPrisoners = totalCount;

            var prisonersPagedList = new StaticPagedList<Prisoner>(listPrisoners, pageNum, pageSize, totalCount);

            return View(prisonersPagedList);
        }

        [Authorize]
        public ActionResult FindPrisonersByName(string search)
        {
            var listPrisoners = prisonerProvider.FindPrisonersByName(search);
            ViewBag.Search = search;
            ViewBag.TotalCountPrisoners = listPrisoners.Count;

            return PartialView("PrisonerPanel", listPrisoners.ToPagedList(1, 4));
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