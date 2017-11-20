using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Domain.Repositories;

namespace Temporary_Prison.Service
{
    public class PrisonService : IPrisonService
    {
      
        public IReadOnlyList<PrisonerProfile> GetPrisoner()
        {
            return null;
        }
    }
}
