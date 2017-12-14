using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IPrisonerDataService
    {
        IReadOnlyList<Prisoner> GetPrisoners();
        bool TryAddPrisoner(Prisoner prisoner, out int newId);
        Prisoner GetPrisonerById(int id);
        IReadOnlyList<Prisoner> GetPrisonersForPageList(int skip, int rowSize, out int totalCount);
    }
}
