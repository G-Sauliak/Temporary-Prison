using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public interface IPrisonerProvider
    {
        IReadOnlyList<Prisoner> GetPrisoner();
        Prisoner GetPrisonerById(int id);
        void AddPrisoner(Prisoner prisoner, out int newId);
        IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount);
    }
}
