using System;
using Temporary_Prison.Data.PrisonService;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonerClient : IPrisonerClient
    {
      
        public PrisonerDto GetPrisonerById(int Id)
        {
            return new PrisonerServiceClient()
                .Execute(client => client.GetPrisonerById(Id));
        }

        public PrisonerDto[] GetPrisonersForPagedList(int skip, int rowSize, out int totalCount, DateTime? filterByDetainedDate, DateTime? filterByReleasedDate)
        {
            int totalCountPrisoners = default(int);

            var prisoners = new PrisonerServiceClient()
                .Execute(client => client.GetPrisonersForPagedList(skip, rowSize, filterByDetainedDate, filterByReleasedDate, out totalCountPrisoners));

            totalCount = totalCountPrisoners;

            return prisoners;
        }

        public void AddPrisoner(PrisonerDto prisoner)
        {
            new PrisonerServiceClient()
                .Execute(client => client.AddPrisoner(prisoner));
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {
            new PrisonerServiceClient()
                .Execute(client => client.RegisterDetention(registrationOfDetention));
        }

        public DetentionPagedListDto[] GetDetentionsByPrisonerIdForPagedList(int Id, int skip, int rowSize, out int totalCount)
        {
            int _totalCount = default(int);
            var detentions = new PrisonerServiceClient()
                .Execute(clinet => clinet.GetDetentionsByPrisonerIdForPagedList(Id, skip, rowSize, out _totalCount));
            totalCount = _totalCount;
            return detentions;
        }

        public DetentionDto GetDetentionById(int id)
        {
            return new PrisonerServiceClient()
                .Execute(client => client.GetDetentionById(id));
        }

        public void DeletePrisoner(int id)
        {
            new PrisonerServiceClient()
                .Execute(client => client.DeletePrisoner(id));
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisonerDto release)
        {
            new PrisonerServiceClient()
                .Execute(client => client.ReleaseOfPrisoner(release));
        }

        public void EditDetention(DetentionDto detentionDto)
        {
            new PrisonerServiceClient()
                 .Execute(client => client.EditDetention(detentionDto));
        }

        public void DeleteDetention(int id)
        {
            new PrisonerServiceClient()
                 .Execute(client => client.DeleteDetention(id));
        }

        public PrisonerDto[] SearchFilter(DateTime? dateOfDetention, string name, string address)
        {
            return new PrisonerServiceClient()
                .Execute(client => client.SearchFilter(dateOfDetention, name, address));
        }

    }
}

