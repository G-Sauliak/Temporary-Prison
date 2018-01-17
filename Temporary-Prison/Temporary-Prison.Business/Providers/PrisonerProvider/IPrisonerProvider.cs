using System;
using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public interface IPrisonerProvider
    {
        Prisoner GetPrisonerById(int id);
        IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount, DateTime? filterByDetainedDate, DateTime? filterByReleasedDate);
        IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address);
        IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, ref int totalCount);
        Detention GetDetentionById(int id);
    }
}
