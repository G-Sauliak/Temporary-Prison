using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IDataService
    {
        IReadOnlyList<PrisonerProfile> GetPrisoners();
    }
}
