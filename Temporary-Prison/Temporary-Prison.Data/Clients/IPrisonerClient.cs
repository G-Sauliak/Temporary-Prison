using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;

namespace Temporary_Prison.Data.Clients
{
    public interface IPrisonerClient
    {
        PrisonerDto GetPrisonerById(int id);
        void AddPrisoner(PrisonerDto prisoner, out int newId);
        IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
    }
}
