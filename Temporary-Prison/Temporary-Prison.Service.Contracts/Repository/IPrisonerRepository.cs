using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    interface IPrisonerRepository
    {
        IReadOnlyList<PrisonerDto> GetPrisoners();
    
        PrisonerDto GetPrisonerById(int Id);
    }
}
