using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System.Runtime.Caching;


namespace Temporary_Prison.Business.Providers
{
    public class PrisonerProvider : IPrisonerProvider
    {
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
            return dataService.GetPrisonerById(id);
        }
    }
}
