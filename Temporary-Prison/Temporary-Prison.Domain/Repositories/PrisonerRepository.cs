using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Domain.Repositories
{
    public class PrisonerRepository : IPrisonerRepository
    {

        public IReadOnlyList<PrisonerProfile> GetPrisoners()
        {
            List<PrisonerProfile> listProfiles = new List<PrisonerProfile>();
            listProfiles.Add(
                new PrisonerProfile {
                    FirstName = "Petya",
                    Patronymic = "Vladimirovich",
                    LastName = "Lomonosov"
                });

            return listProfiles;
        }
    }
}
