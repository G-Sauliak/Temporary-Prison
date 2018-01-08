using AutoMapper;
using System.Collections.Generic;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.PrisonService;
using log4net;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Data.Services
{
    public class PrisonerDataService : IPrisonerDataService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");
        private readonly IPrisonerClient prisonerClient;

        public PrisonerDataService() : this(new PrisonerClient())
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Prisoner, PrisonerDto>();
                cfg.CreateMap<RegistDetention, RegistrationOfDetentionDto>();
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<DetentionPagedList, DetentionPagedListDto>();
                cfg.CreateMap<Detention, DetentionDto>();
                cfg.CreateMap<DetentionDto, Detention>();
                cfg.CreateMap<ReleaseOfPrisoner, ReleaseOfPrisonerDto>();
            });
        }

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

        public IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
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
            if (prisoner != null)
            {
                var prisonerDto = Mapper.Map<Prisoner, PrisonerDto>(prisoner);
                return prisonerClient.AddPrisoner(prisonerDto, out newId);
            }
            newId = default(int);
            return default(bool);
        }

        public IReadOnlyList<Prisoner> FindPrisonersByName(string search)
        {
            var prisonersDto = prisonerClient.FindPrisonersByName(search);

            if (prisonersDto != null)
            {
                try
                {
                    var prisoners = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);
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
            prisonerClient.DeletePrisoner(id);
        }

        public void RegisterDetention(RegistDetention registrationOfDetention)
        {

            var registDeten = Mapper.Map<RegistDetention, RegistrationOfDetentionDto>(registrationOfDetention);
            prisonerClient.RegisterDetention(registDeten);
        }

        public IReadOnlyList<DetentionPagedList> GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount)
        {
            var detentionsDto = prisonerClient.GetDetentionsByPrisonerIdForPagedList(Id, skip, rowSize, out totalCount);
            if (detentionsDto != null)
            {
                var detentions = Mapper.Map<DetentionPagedListDto[], DetentionPagedList[]>(detentionsDto);
                return detentions;
            }
            return default(DetentionPagedList[]);
        }

        public Detention GetDetentionById(int id)
        {
            var detentionDto = prisonerClient.GetDetentionById(id);
            if (detentionDto != null)
            {
                var detention = Mapper.Map<DetentionDto, Detention>(detentionDto);
                return detention;
            }
            return default(Detention);
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisoner release)
        {
            var releaseOfPrisonerDto = Mapper.Map<ReleaseOfPrisoner, ReleaseOfPrisonerDto>(release);
            prisonerClient.ReleaseOfPrisoner(releaseOfPrisonerDto);
        }

        public void EditDetention(Detention detention)
        {
            var detentionDto = Mapper.Map<Detention, DetentionDto>(detention);
            prisonerClient.EditDetention(detentionDto);
        }

        public void DeleteDetention(int id)
        {
            prisonerClient.DeleteDetention(id);
        }
    }
}
