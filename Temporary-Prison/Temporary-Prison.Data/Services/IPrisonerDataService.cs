using System.Collections.Generic;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IPrisonerDataService
    {
        bool AddPrisoner(Prisoner prisoner, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        Prisoner GetPrisonerById(int id);
        IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);
        IReadOnlyList<Prisoner> FindPrisonersByName(string search);
        void RegisterDetention(RegistDetention registDetention);
        IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount);
    }
}
