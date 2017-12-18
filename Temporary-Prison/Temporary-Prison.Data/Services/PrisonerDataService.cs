using AutoMapper;
using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.PrisonService;

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

        public IReadOnlyList<Prisoner> GetPrisonersForPageList(int skip, int rowSize, out int totalCount)
        {
            var prisonersDto = prisonerClient.GetPrisonersForPagedList(skip, rowSize, out totalCount);
            if (prisonersDto != null)
            {
                var prisoners = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);

                return prisoners;
            }
            return default(IReadOnlyList<Prisoner>);
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

        public bool AddPrisoner(Prisoner prisoner, out int newId)
        {
            var result = false;
            if (prisoner != null)
            {
                var prisonerDto = Mapper.Map<Prisoner, PrisonerDto>(prisoner);
                int _newId = default(int);
                result = new PrisonerServiceClient().
                    Execute(client => 
                    client.AddPrisoner(prisonerDto, out _newId));
                newId = _newId;

                return result;
            }
            newId = default(int);
            return default(bool);
        }
    }
}
