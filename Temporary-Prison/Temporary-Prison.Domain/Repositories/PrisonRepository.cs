using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Domain.Repositories
{
    public class PrisonRepository : IPrisonRepository
    {

        public IReadOnlyList<Prisoner> GetPrisoners()
        {
            List<Prisoner> listProfiles = new List<Prisoner>();
            listProfiles.Add(
                new Prisoner {
                    FirstName = "Petya",
                    Surname = "Vladimirovich",
                    LastName = "Lomonosov"
                });

            return listProfiles;
        }
    }
}
