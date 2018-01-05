using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;

namespace Temporary_Prison.Data.Clients
{
    public interface IPrisonerClient
    {
        PrisonerDto GetPrisonerById(int id);
        bool AddPrisoner(PrisonerDto prisoner, out int newId);
        IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
        void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention);
        IReadOnlyList<PrisonerDto> FindPrisonersByName(string search);
        DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount);
        DetentionDto GetDetentionById(int id);
        void DeletePrisoner(int id);
    }
}
