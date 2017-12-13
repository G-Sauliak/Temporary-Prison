using AutoMapper;
using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Services
{
    public class PrisonerDataService : IPrisonerDataService
    {
        private readonly IPrisonerClient prisonerClient;

        public PrisonerDataService(IPrisonerClient prisonerClient)
        {
            this.prisonerClient = prisonerClient;
        }

        public Prisoner GetPrisonerById(int id)
        {
            var prisonerDto = prisonerClient.GetPrisonerById(id);

            if (prisonerDto != null)
            {
                var prisoner = Mapper.Map<PrisonerDto, Prisoner>(prisonerDto);

                return prisoner;
            }

            return default(Prisoner);
        }

        public IReadOnlyList<Prisoner> GetPrisoners()
        {
            var prisonersDto = prisonerClient.GetPrisoners();

            if (prisonersDto != null)
            {
                var prisoners = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);

                return prisoners;
            }

            return default(IReadOnlyList<Prisoner>);
        }

        public bool TryAddPrisoner(Prisoner prisoner, out int newId)
        {
            if (prisoner != null)
            {
                var prisonerDto = Mapper.Map<Prisoner, PrisonerDto>(prisoner);

                return prisonerClient.TryAddPrisoner(prisonerDto, out newId);
            }
            newId = default(int);
            return default(bool);
        }
    }
}
