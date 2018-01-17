using System;
using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;

namespace Temporary_Prison.Data.Clients
{
    public interface IPrisonerClient
    {
        PrisonerDto GetPrisonerById(int id);
        bool AddPrisoner(PrisonerDto prisoner, out int newId);
        IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount, DateTime? filterByDetainedDate, DateTime? filterByReleasedDate);
        void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention);
        IReadOnlyList<PrisonerDto> SearchFilter(DateTime? dateOfDetention, string name, string address);
        DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount);
        DetentionDto GetDetentionById(int id);
        void DeletePrisoner(int id);
        void ReleaseOfPrisoner(ReleaseOfPrisonerDto release);
        void EditDetention(DetentionDto detention);
        void DeleteDetention(int id);
    }
}
