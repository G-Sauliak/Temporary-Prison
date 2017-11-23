using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Converters
{
    public class ConvertPrisoners : IConvertPrisoners
    {
        public IReadOnlyList<PrisonerProfile> ToListPrisonProfile(IReadOnlyList<PrisonerProfileDto> prisoners)
        {

            List<PrisonerProfile> listPrisoners = new List<PrisonerProfile>();

            foreach (var prisoner in prisoners)
            {
                listPrisoners.Add(
                    new PrisonerProfile() {
                        FirstName = prisoner.FirstName,
                        LastName = prisoner.LastName,
                        Patronymic = prisoner.Patronymic,
                    }
                    );
            }

            return listPrisoners;
            
        }
    }
}
