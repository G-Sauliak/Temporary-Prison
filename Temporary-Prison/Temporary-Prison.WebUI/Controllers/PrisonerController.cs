using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult ListOfPrisoners(int? page, string sort, string currentFilter, string search,int? currentTotal)
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

            ViewBag.TotalCountPrisoners = totalCount;

            var listPrisoners = prisonerProvider.GetPrisonerForPagedList(skip, pageSize, ref totalCount);
            var prisonersPagedList = new StaticPagedList<Prisoner>(listPrisoners, pageNum, pageSize, totalCount);

            return View(prisonersPagedList);
        }

        // Prisoner: DetailsOfPrisoner
        [HttpGet]
        [Authorize]
        public ActionResult DetailsOfPrisoner(int? id)
        {
            if (!id.HasValue)
            {
                RedirectToAction("ListPrisoner");
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);

            return View(prisoner);
        }

    }
}