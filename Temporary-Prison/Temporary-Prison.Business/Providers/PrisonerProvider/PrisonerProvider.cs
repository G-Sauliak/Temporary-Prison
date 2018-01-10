using System.Collections.Generic;
using Temporary_Prison.Data.Services;
using System;
using Temporary_Prison.Business.CacheManager;
using log4net;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public class PrisonerProvider : IPrisonerProvider
    {
        private readonly IPrisonerDataService dataService;
        private readonly ICacheService cacheService;
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerProvider() : this(new PrisonerDataService(), new CacheService())
        {

        }

        public PrisonerProvider(IPrisonerDataService prisonerDataService, ICacheService cacheService)
        {
            this.cacheService = cacheService;
            this.dataService = prisonerDataService;
        }

        public Prisoner GetPrisonerById(int id)
        {
            var cacheKey = $"prisoner_{id}";
            var priosner = default(Prisoner);
            try
            {
                priosner = cacheService.GetOrSet(cacheKey,
                    () => dataService.GetPrisonerById(id));

                return priosner;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            return default(Prisoner);
        }

        public IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, ref int totalCount, string search)
        {
            var cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";
            var outTotalCount = default(int);
            var listPrisoners = default(IReadOnlyList<Prisoner>);

            /*if (!string.IsNullOrEmpty(search))
              {
                  var cacheKey = $"FPBN:{search}";
                  listPrisoners = cacheService.GetOrSet(cacheKey,
                      () => SearchFilter(search), DateTime.Now.AddSeconds(20));
                  return listPrisoners;
              }
              else
              {*/
            listPrisoners = cacheService.GetOrSet(cacheKeyForPageList,
                () => dataService.GetPrisonersForPagedList(skip, rowSize, out outTotalCount), DateTime.Now.AddSeconds(20));

            if (totalCount == default(int))
            {
                cacheService.Remove(cacheKeyForPageList);
            }
            if (outTotalCount != default(int))
            {
                totalCount = outTotalCount;
            }
            // }
            return listPrisoners;
        }

        public void AddPrisoner(Prisoner prisoner, out int newId)
        {
            dataService.AddPrisoner(prisoner, out newId);
        }


        public void EditPrisoner(Prisoner prisoner)
        {
            cacheService.Remove($"prisoner_{prisoner.PrisonerId}");
            dataService.EditPrisoner(prisoner);
        }

        public void DeletePrisoner(int id)
        {
            dataService.DeletePrisoner(id);
        }

        public void RegisterDetention(RegistDetention registDetention)
        {
            dataService.RegisterDetention(registDetention);
        }

        public IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, ref int totalCount)
        {
            return dataService.GetDetentionsByPrisonerIdForPagedList(Id, skip, rowSize, out totalCount);
        }

        public Detention GetDetentionById(int id)
        {
            var cacheKey = $"Detention_{id}";

            var detention = cacheService.GetOrSet(cacheKey,
                () => dataService
                .GetDetentionById(id), DateTime.Now.AddMinutes(3));

            return detention;
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisoner release)
        {
            dataService.ReleaseOfPrisoner(release);
            cacheService.Remove($"Detention_{release.DetentionID}");
        }

        public void EditDetention(Detention detention)
        {
            dataService.EditDetention(detention);
            cacheService.Remove($"Detention_{detention.DetentionID}");
        }

        public void DeleteDetention(int id)
        {
            dataService.DeleteDetention(id);
        }

        public IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address)
        {
            return dataService.SearchFilter(dateOfDetention, name, address);
        }
    }
}
