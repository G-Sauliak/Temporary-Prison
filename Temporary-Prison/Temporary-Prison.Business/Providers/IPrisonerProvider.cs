using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public interface IPrisonerProvider
    {
        Prisoner GetPrisonerById(int id);
        void AddPrisoner(Prisoner prisoner, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount, string srarch);
        IReadOnlyList<Prisoner> FindPrisonersByName(string search);
        void RegisterDetention(RegistDetention registDetention);
        IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, ref int totalCount);
        Detention GetDetentionById(int id);
    }
}
