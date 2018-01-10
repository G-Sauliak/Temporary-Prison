using System;
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
        void ReleaseOfPrisoner(ReleaseOfPrisoner release);
        void RegisterDetention(RegistDetention registDetention);
        IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, ref int totalCount, string srarch);
        IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address);
        IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, ref int totalCount);
        Detention GetDetentionById(int id);
        void EditDetention(Detention detention);
        void DeleteDetention(int id);
    }
}
