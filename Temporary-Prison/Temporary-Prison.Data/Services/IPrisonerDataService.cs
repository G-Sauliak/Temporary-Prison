using System.Collections.Generic;
using System.Threading.Tasks;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public interface IPrisonerDataService
    {
        IReadOnlyList<Prisoner> GetPrisoners();
        bool AddPrisoner(Prisoner prisoner, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        Prisoner GetPrisonerById(int id);
        IReadOnlyList<Prisoner> GetPrisonersForPageList(int skip, int rowSize, out int totalCount);
        Task<IReadOnlyList<Prisoner>> FindPrisonersByName(string search);

    }
}
