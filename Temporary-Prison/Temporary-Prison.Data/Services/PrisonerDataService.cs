using AutoMapper;
using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.PrisonService;
using log4net;

namespace Temporary_Prison.Data.Services
{
    public class PrisonerDataService : IPrisonerDataService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
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
            return result;
        }

        public IReadOnlyList<Prisoner> FindPrisonersByName(string search)
        {
            var prisonersDto = new PrisonerServiceClient().Execute(clinet => clinet.FindPrisonersByName(search));

            if (prisonersDto != null)
            {
                try
                {
                    var prisoners = Mapper.Map<PrisonerDto[], Prisoner[]>(prisonersDto);
                    return prisoners;

                }
                catch (AutoMapperMappingException me)
                {
                    log.Error(me.Message);
                }
            }
            return default(Prisoner[]);
        }
        public void EditPrisoner(Prisoner prisoner)
        {
            if (prisoner != null)
            {
                var prisonerDto = Mapper.Map<Prisoner, PrisonerDto>(prisoner);
                new PrisonerServiceClient().Execute(client => client.EditPrisoner(prisonerDto));
            }
        }

        public void DeletePrisoner(int id)
        {
            new PrisonerServiceClient().Execute(client => client.DeletePrisoner(id));
        }
    }
}
