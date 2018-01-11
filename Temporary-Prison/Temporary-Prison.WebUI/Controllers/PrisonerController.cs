using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Temporary_Prison.Attributes;
using Temporary_Prison.Business.Providers;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Dependencies.MapperRegistry;
using Temporary_Prison.WebMapperProfile;
using Temporary_Prison.Models;
using Temporary_Prison.SiteConfigService;
using X.PagedList;

namespace Temporary_Prison.Controllers
{
    [Authorize]
    public class PrisonerController : Controller
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IPrisonerProvider prisonerProvider;
        private readonly IConfigService configService;
        public PrisonerController() : this(new PrisonerProvider(), new ConfigService())
        {
            Dependencies.MapperRegistry.MapperProfiles.Configuration.AddProfile(new WebMapper());
            Dependencies.MapperRegistry.MapperProfiles.InitialiseMappers();
        }

        public PrisonerController(IPrisonerProvider prisonerProvider,IConfigService configService)
        {
            this.configService = configService;
            this.prisonerProvider = prisonerProvider;
        }

        // GET: Prisoner/ListOfPrisoners
        [HttpGet]
        public ActionResult ListOfPrisoners(int? page, int? totalCount)
        {
            var pageSize = configService.PrisonerPagedSize;
            var _totalCount = totalCount ?? default(int);

            var pageNum = page ?? 1;
            var skip = (pageNum - 1) * pageSize;

            var listPrisoners = prisonerProvider.
                GetPrisonersForPagedList(skip, pageSize, ref _totalCount);

            ViewBag.TotalCountPrisoners = _totalCount;

            var model = Mapper.Map<IReadOnlyList<Prisoner>, IReadOnlyList<PrisonerPagedListViewModel>>(listPrisoners);

            if (model != null)
            {
                var staticPagedListOfPrisoners = new StaticPagedList<PrisonerPagedListViewModel>(model, pageNum, pageSize, _totalCount);

                return View(staticPagedListOfPrisoners);
            }
            
            return View(default(StaticPagedList<Prisoner>));
        }

        [HttpGet]
        public ActionResult InmateSearches()
        {
            return View(new SearchFilterModelView());
        }

        [AjaxOnly]
        public ActionResult SearchFilter(DateTime? DateOfDetention, string Name, string Address)
        {
            if (DateOfDetention != null || !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Address))
            {
                var foundPrisoners = prisonerProvider.SearchFilter(DateOfDetention, Name, Address);
                if (foundPrisoners != null)
                {
                    var model = Mapper.Map<IReadOnlyList<Prisoner>, IReadOnlyList<PrisonerPagedListViewModel>>(foundPrisoners);

                    return PartialView("SearchResultOfInmatesPartial", model.ToPagedList(1, configService.PrisonerPagedSize));
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        //GET: Prisoner/DetailsOfPrisoner
        [HttpGet]
        public ActionResult DetailsOfPrisoner(int? id, int? page, int? totalCountDetentions)
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