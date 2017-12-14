using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    public interface IPrisonerRepository
    {
        IReadOnlyList<PrisonerDto> GetPrisoners();
    
        PrisonerDto GetPrisonerById(int Id);

        bool TryAddPrisoner(PrisonerDto prisoner, out int newId);

        IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
    }
}
