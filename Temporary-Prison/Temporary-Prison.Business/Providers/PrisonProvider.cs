using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Services;


namespace Temporary_Prison.Business
{
    public class PrisonProvider : IPrisonProvider
    {
        private readonly IDataService dataService;

        public PrisonProvider(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public IReadOnlyList<PrisonerProfile> GetPrisoner()
        {

            return dataService.GetPrisoners();
        }
    }
}
