using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    public class PrisonerController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        public PrisonerController() : this(new PrisonerProvider())
        {

        }

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
            var skip = (pageNum - 1) * pageSize;

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
        public ActionResult DetailsOfPrisoner(int? id, int? page, int? currentTotal)
        {
            const int pageSize = 4;

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;

            var totalCount = default(int);
            var _currentTotal = currentTotal ?? default(int);

            if (_currentTotal != default(int))
            {
                totalCount = _currentTotal;
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);
            var model = Mapper.Map<Prisoner, DetailsPrisonerViewModel>(prisoner);

            var listOfDetentions = prisonerProvider.GetDetentionsByPrisonerIdForPagedList(id.Value, skip, pageSize, ref totalCount);

            ViewBag.Guarded = listOfDetentions.Last().DateOfRelease != null ? true : false;

            var pagedListDetention = new StaticPagedList<DetentionPagedList>(listOfDetentions, pageNum, pageSize, totalCount);

            model.DetentionPagedList = pagedListDetention;

            return View(model);
        }

    }
}