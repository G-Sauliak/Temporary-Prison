using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Converters
{
    public class PrisonServiceConvert : IPrisonServiceConvert
    {
        public IReadOnlyList<Prisoner> ToListPrisoners(IReadOnlyList<PrisonerDto> prisonersDto)
        {

            List<Prisoner> listPrisoners = new List<Prisoner>();

            foreach (var prisonerDto in prisonersDto)
            {
                listPrisoners.Add(
                    new Prisoner() {
                        PrisonerId = prisonerDto.PrisonerId,
                        FirstName = prisonerDto.FirstName,
                        LastName = prisonerDto.LastName,
                        Surname = prisonerDto.Surname,
                        AdditionalInformation = prisonerDto.AdditionalInformation,
                        BirthDate = prisonerDto.BirthDate,
                        PlaceOfWork = prisonerDto.PlaceOfWork,
                        RelationshipStatus = prisonerDto.RelationshipStatus,
                        PhoneNumbers = prisonerDto.PhoneNumbers,
                    }
                    );
            }

            return listPrisoners;
            
        }
    }
}
