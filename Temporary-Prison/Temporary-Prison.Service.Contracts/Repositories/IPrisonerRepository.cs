using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Repositories
{
    public interface IPrisonerRepository
    {
        IReadOnlyList<PrisonerDto> GetPrisoners();
        PrisonerDto GetPrisonerById(int Id);
        bool AddPrisoner(PrisonerDto prisoner, out int newId);
        IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
        PrisonerDto[] FindPrisonersByName(string search);
        void EditPrisoner(PrisonerDto prisoner);
        void DeletePrisoner(int id);
    }
}
