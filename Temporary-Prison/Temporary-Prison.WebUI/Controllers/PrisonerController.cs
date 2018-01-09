using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Temporary_Prison.Attributes;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Dependencies.MapperRegistry;
using Temporary_Prison.MapperProfile;
using Temporary_Prison.Models;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    [Authorize]
    public class PrisonerController : Controller
    {
        private readonly IPrisonerProvider prisonerProvider;
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        public PrisonerController() : this(new PrisonerProvider())
        {
            MapperProfiles.Configuration.AddProfile(new WebMapper());
            MapperProfiles.InitialiseMappers();
        }

        public PrisonerController(IPrisonerProvider prisonerProvider)
        {
            this.prisonerProvider = prisonerProvider;
        }

        // GET: Prisoner/ListOfPrisoners
        [HttpGet]
        [Route("Prisoner/ListOfPrisoners/{page:int}")]
        public ActionResult ListOfPrisoners(int? page, int? totalCount, string search)
        {
            const int pageSize = 4;
            var _totalCount = totalCount ?? default(int);

            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;

            var listPrisoners = prisonerProvider.
                GetPrisonerForPagedList(skip, pageSize, ref _totalCount, search);

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                return View(listPrisoners.ToPagedList(pageNum, pageSize));
            }
            ViewBag.TotalCountPrisoners = _totalCount;
            
            var model = Mapper.Map<IReadOnlyList<Prisoner>, IReadOnlyList<PrisonerPagedListViewModel>>(listPrisoners);

            if (listPrisoners != null)
            {
                var prisonersPagedList = new StaticPagedList<PrisonerPagedListViewModel>(model, pageNum, pageSize, _totalCount);
                return View(prisonersPagedList);
            }
            return View(default(StaticPagedList<Prisoner>));
        }

        [AjaxOnly]
        public ActionResult FindPrisonersByName(string search)
        {
            var listPrisoners = prisonerProvider.FindPrisonersByName(search);
            var listViewModel = Mapper.Map<IReadOnlyList<Prisoner>, IReadOnlyList<PrisonerPagedListViewModel>>(listPrisoners);
            ViewBag.Search = search;
            ViewBag.TotalCountPrisoners = listPrisoners.Count;

            return PartialView("PrisonerPanel", listViewModel.ToPagedList(1, 2));
        }
        //GET: Prisoner/DetailsOfPrisoner
        [HttpGet]
        public ActionResult DetailsOfPrisoner(int?  id, int? page, int? totalCountDetentions)
        {
            const int pageSize = 4;

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prisoner = prisonerProvider.GetPrisonerById(id.Value);

            if (prisoner == null)
            {
                return HttpNotFound();
            }
            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;
            var _currentTotal = totalCountDetentions ?? default(int);

            var model = Mapper.Map<Prisoner, DetailsPrisonerViewModel>(prisoner);

            var listOfDetentions = prisonerProvider
                .GetDetentionsByPrisonerIdForPagedList(id.Value, skip, pageSize, ref _currentTotal);

            if (listOfDetentions != null)
            {
                var listOfDetentionsModel = Mapper
                    .Map<IReadOnlyList<DetentionPagedList>, IReadOnlyList<DetentionPagedListViewModel>>(listOfDetentions);

                ViewBag.prisonerId = model.PrisonerId;
                ViewBag.currentPage = pageNum;
                ViewBag.Guarded = listOfDetentions.Last().DateOfRelease != null ? false : true;
                ViewBag.totalCountDetentions = _currentTotal;

                var pagedListDetention = new StaticPagedList<DetentionPagedListViewModel>(listOfDetentionsModel, pageNum, pageSize, _currentTotal);
                model.DetentionPagedList = pagedListDetention;
            }
            else
            {
                ViewBag.Guarded = false;
            }

            return View(model);
        }

        public ActionResult DetailsOfDetention(int? id)
        {
            if (!id.HasValue || id.Value == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detention = prisonerProvider.GetDetentionById(id.Value);

            if (detention == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<Detention, DetailsOfDetentionViewModel>(detention);

            return View(model);
        }

    }
}