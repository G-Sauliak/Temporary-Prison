using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System.Web;
using System;
using System.Web.Caching;

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
            string cacheKey = $"prisoner_{id}";

            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (Prisoner)HttpRuntime.Cache[cacheKey];
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
            catch
            {
                //TODO
            }

            return default(Prisoner);
        }

        public bool TryAddPrisoner(Prisoner prisoner, out int newId)
        {
            return dataService.TryAddPrisoner(prisoner, out newId);
        }
    }
}
