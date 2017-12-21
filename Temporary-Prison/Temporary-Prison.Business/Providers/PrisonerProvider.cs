using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System;
using Temporary_Prison.Business.CacheManager;
using log4net;

namespace Temporary_Prison.Business.Providers
{
    public class PrisonerProvider : IPrisonerProvider
    {
        private readonly IPrisonerDataService prisonerDataService;
        private readonly ICacheService cacheService;
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerProvider(IPrisonerDataService dataService, ICacheService cacheService)
        {
            this.cacheService = cacheService;
            this.prisonerDataService = dataService;
        }

        public IReadOnlyList<Prisoner> GetPrisoner()
        {
            return prisonerDataService.GetPrisoners();
        }

        public Prisoner GetPrisonerById(int id)
        {
            var cacheKey = $"prisoner_{id}";
            var priosner = default(Prisoner);
            try
            {
                priosner = cacheService.GetOrSet(cacheKey,
                    () => prisonerDataService.GetPrisonerById(id));

                return priosner;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            return default(Prisoner);
        }

        public IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount, string search)
        {
            var cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";
            var outTotalCount = default(int);
            var listPrisoners = default(IReadOnlyList<Prisoner>);
            try
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var cacheKey = $"FPBN:{search}";
                    listPrisoners = cacheService.GetOrSet(cacheKey,
                        () => FindPrisonersByName(search));
                    return listPrisoners;
                }
                else
                {

                    listPrisoners = cacheService.GetOrSet(cacheKeyForPageList,
                        () => prisonerDataService.
                        GetPrisonersForPageList(skip, rowSize, out outTotalCount), DateTime.Now.AddSeconds(30));


                    if (totalCount == default(int))
                    {
                        cacheService.Remove(cacheKeyForPageList);
                    }
                    if (outTotalCount != default(int))
                    {
                        totalCount = outTotalCount;
                    }

                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            return listPrisoners;
        }

        public void AddPrisoner(Prisoner prisoner, out int newId)
        {
            prisonerDataService.AddPrisoner(prisoner, out newId);
        }

        public IReadOnlyList<Prisoner> FindPrisonersByName(string search)
        {
            var cacheKey = $"FPBN:{search}";
            var listPrisoner = cacheService.GetOrSet(cacheKey,
                () => prisonerDataService.FindPrisonersByName(search),
                DateTime.Now.AddMinutes(2));

            return listPrisoner;
        }

        public void EditPrisoner(Prisoner prisoner)
        {
            cacheService.Remove($"prisoner_{prisoner.PrisonerId}");
            prisonerDataService.EditPrisoner(prisoner);
        }

        public void DeletePrisoner(int id)
        {
            prisonerDataService.DeletePrisoner(id);
        }
    }
}
