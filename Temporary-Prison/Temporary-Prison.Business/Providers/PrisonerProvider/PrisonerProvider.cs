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
        private readonly IPrisonerDataService prisonerDataService;
        private readonly ICacheService cacheService;
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerProvider() : this(new PrisonerDataService(), new CacheService())
        { }

        public PrisonerProvider(IPrisonerDataService prisonerDataService, ICacheService cacheService)
        {
            this.cacheService = cacheService;
            this.prisonerDataService = prisonerDataService;
        }

        public Prisoner GetPrisonerById(int id)
        {
            var cacheKey = $"prisoner_{id}";

            return cacheService.GetOrSet(cacheKey, () => prisonerDataService.GetPrisonerById(id));

        }

        public IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, ref int totalCount)
        {
            var cacheKeyForPageList = $"prisonersForPagelist_s_{skip}_r_{rowSize}_t_{totalCount}";
            var outTotalCount = default(int);
            var listPrisoners = default(IReadOnlyList<Prisoner>);

            listPrisoners = cacheService.GetOrSet(cacheKeyForPageList, () =>
            prisonerDataService.GetPrisonersForPagedList(skip, rowSize, out outTotalCount), TimeSpan.FromSeconds(10));

            if (totalCount == default(int))
            {
                cacheService.Remove(cacheKeyForPageList);
            }
            if (outTotalCount != default(int))
            {
                totalCount = outTotalCount;
            }

            return listPrisoners;
        }

        public IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, ref int totalCount)
        {
           
          //  var cacheKeyForPageList = $"GetDBPIdForPagedList_s_{skip}_r_{rowSize}_t_{totalCount}";
         //   var outParam = totalCount;
            return prisonerDataService.GetDetentionsByPrisonerIdForPagedList(Id, skip, rowSize, out totalCount);
        }

        public Detention GetDetentionById(int id)
        {
            var cacheKey = $"Detention_{id}";

            var detention = cacheService.GetOrSet(cacheKey, () => prisonerDataService.GetDetentionById(id));

            return detention;
        }

        public IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address)
        {
            return prisonerDataService.SearchFilter(dateOfDetention, name, address);
        }
    }
}
