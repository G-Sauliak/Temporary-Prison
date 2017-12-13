using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    interface IPrisonerRepository
    {
        IReadOnlyList<PrisonerDto> GetPrisoners();
    
        PrisonerDto GetPrisonerById(int Id);

        bool TryAddPrisoner(PrisonerDto prisoner, out int newId);
    }
}
