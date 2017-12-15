using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System;
using Temporary_Prison.Business.CacheManager;

namespace Temporary_Prison.Business.Providers
{
    public class PrisonerProvider : IPrisonerProvider
    {
        private readonly IPrisonerDataService dataService;
        private readonly ICacheService cacheService;

        public PrisonerProvider(IPrisonerDataService dataService, ICacheService cacheService)
        {
            this.cacheService = cacheService;
            this.dataService = dataService;
        }

        public IReadOnlyList<Prisoner> GetPrisoner()
        {
            return dataService.GetPrisoners();
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
                //TODO
            }
            return default(Prisoner);
        }

        public IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount)
        {
            var cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";
            var outTotalCount = default(int);
            var prisoners = default(IReadOnlyList<Prisoner>);
            try
            {
                prisoners = cacheService.GetOrSet(cacheKeyForPageList,
                    () => dataService.GetPrisonersForPageList(skip, rowSize, out outTotalCount));

                if (totalCount == default(int))
                {
                    cacheService.Remove(cacheKeyForPageList);
                }
                totalCount = outTotalCount;

            }
            catch (Exception ex)
            {
                //TODO
            }
            return prisoners;
        }

        public bool TryAddPrisoner(Prisoner prisoner, out int newId)
        {
            return dataService.TryAddPrisoner(prisoner, out newId);
        }
    }
}
