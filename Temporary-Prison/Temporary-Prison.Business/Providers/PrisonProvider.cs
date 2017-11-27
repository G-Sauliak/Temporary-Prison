using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;
using System.Runtime.Caching;


namespace Temporary_Prison.Business
{
    public class PrisonProvider : IPrisonProvider
    {
        private readonly IDataService dataService;

        public PrisonProvider(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public IReadOnlyList<Prisoner> GetPrisoner()
        {
            return dataService.GetPrisoners();
        }
    }
}
