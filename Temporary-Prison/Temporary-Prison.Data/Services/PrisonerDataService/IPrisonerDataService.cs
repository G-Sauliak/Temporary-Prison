using System;
using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IPrisonerDataService
    {
        bool AddPrisoner(Prisoner prisoner, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        void ReleaseOfPrisoner(ReleaseOfPrisoner release);
        Prisoner GetPrisonerById(int id);
        IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
        IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address);
        void RegisterDetention(RegistDetention registDetention);
        IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount);
        Detention GetDetentionById(int id);
        void EditDetention(Detention detention);
        void DeleteDetention(int id);

    }
}
