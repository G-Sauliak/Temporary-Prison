using AutoMapper;
using System.Collections.Generic;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.PrisonService;
using log4net;
using Temporary_Prison.Common.Models;
using System;
using Temporary_Prison.Data.MapperProfile;

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
                cfg.AddProfile(new DataMapper());
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

        public IReadOnlyList<Prisoner> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount,
            DateTime? filterByDetainedDate, DateTime? filterByReleasedDate)
        {
            var prisonersDto = prisonerClient.GetPrisonersForPagedList(skip, rowSize, out totalCount, filterByDetainedDate, filterByReleasedDate);
            if (prisonersDto != null)
            {
                var prisoners = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);

                return prisoners;
            }
            return default(IReadOnlyList<Prisoner>);
        }

        public void AddPrisoner(Prisoner prisoner)
        {
          
            var prisonerDto = Mapper.Map<Prisoner, PrisonerDto>(prisoner);
            prisonerClient.AddPrisoner(prisonerDto);
            
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

        public IReadOnlyList<Prisoner> SearchFilter(DateTime? dateOfDetention, string name, string address)
        {
            var prisonersDto = prisonerClient.SearchFilter(dateOfDetention, name, address);
            if(prisonersDto!= null)
            {
                var prisoners = Mapper.Map<IReadOnlyList<PrisonerDto>, IReadOnlyList<Prisoner>>(prisonersDto);
                return prisoners;
            }
            return default(IReadOnlyList<Prisoner>);
        }
    }
}
