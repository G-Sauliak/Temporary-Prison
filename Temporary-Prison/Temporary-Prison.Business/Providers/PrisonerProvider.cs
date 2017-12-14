using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System.Web;
using System;
using System.Web.Caching;
using System.ServiceModel;

namespace Temporary_Prison.Business.Providers
{
    public class PrisonerProvider : IPrisonerProvider
    {
        private const int _cacheTimeoutInMinute = 30;

        private readonly IPrisonerDataService dataService;

        public PrisonerProvider(IPrisonerDataService dataService)
        {
            this.dataService = dataService;
        }

        public IReadOnlyList<Prisoner> GetPrisoner()
        {
            return dataService.GetPrisoners();
        }

        public Prisoner GetPrisonerById(int id)
        {
            var cacheKey = $"prisoner_{id}";

            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return HttpRuntime.Cache[cacheKey] as Prisoner;
            }
            
            try
            {
                var prisoner = dataService.GetPrisonerById(id);

                if (prisoner != null)
                {
                    cacheKey = $"prisoner_{prisoner.PrisonerId}";

                    HttpRuntime.Cache.Insert(cacheKey, prisoner, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);

                    return prisoner;
                }
            }
            catch(Exception ex)
            {
                //TODO
            }

            return default(Prisoner);
        }

        public IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount)
        {

            var cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";

            if (HttpRuntime.Cache[cacheKeyForPageList] != null)
            {
                var cacheDataPrisoners = HttpRuntime.Cache[cacheKeyForPageList] as IReadOnlyList<Prisoner>;
                return cacheDataPrisoners;
            }

            try
            {
                var prisonersForPageList = dataService.GetPrisonersForPageList(skip,rowSize, out totalCount);

                if (prisonersForPageList != null)
                {
                    var _cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";

                    HttpRuntime.Cache.Insert(_cacheKeyForPageList, prisonersForPageList, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);

                    return prisonersForPageList;
                }
            }
            catch (Exception ex)
            {
                //TODO
            }

            return default(IReadOnlyList<Prisoner>);
        }

        public bool TryAddPrisoner(Prisoner prisoner, out int newId)
        {
            return dataService.TryAddPrisoner(prisoner, out newId);
        }
    }
}
