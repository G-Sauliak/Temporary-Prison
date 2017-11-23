using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonService : IPrisonService
    {
        public List<PrisonerProfileDto> GetPrisoners()
        {
            List<PrisonerProfileDto> listProfiles = new List<PrisonerProfileDto>();

            listProfiles.Add(
                new PrisonerProfileDto
                {
                    UserId = 1,
                    FirstName = "Petya",
                    Patronymic = "Vladimirovich",
                    LastName = "Lomonosov"
                });
            listProfiles.Add(
                new PrisonerProfileDto
                {
                    UserId =2,
                    FirstName = "Grisha",
                    Patronymic = "Ptrivich",
                    LastName = "Soveliev"

                });

            return listProfiles;
        }
    }
}
