using System.Collections.Generic;
using System.Threading.Tasks;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business.Providers
{
    public interface IPrisonerProvider
    {
        IReadOnlyList<Prisoner> GetPrisoner();
        Prisoner GetPrisonerById(int id);
        void AddPrisoner(Prisoner prisoner, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        IReadOnlyList<Prisoner> GetPrisonerForPagedList(int skip, int rowSize, ref int totalCount);
        Task<IReadOnlyList<Prisoner>> FindPrisonersByName(string search);
    }
}
