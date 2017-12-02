using AutoMapper;
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

            var prisoner = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);

            return prisoner;

        }
    }
}
