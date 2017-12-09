using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public interface IPrisonerClient
    {
        IReadOnlyList<PrisonerDto> GetPrisoners();

        PrisonerDto GetPrisonerById(int id);
    }
}
