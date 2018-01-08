using System.Collections.Generic;
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

        public IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
        {
            int totalCountPrisoners = default(int);

            var prisoners = new PrisonerServiceClient()
                .Execute(client => client.GetPrisonersForPagedList(skip, rowSize, out totalCountPrisoners));

            totalCount = totalCountPrisoners;

            return prisoners;
        }

        public bool AddPrisoner(PrisonerDto prisoner, out int newId)
        {
            int _newId = default(int);
            var result = new PrisonerServiceClient()
                .Execute(client => client.AddPrisoner(prisoner, out _newId));
            newId = _newId;
            return result;
        }

        public void RegisterDetention(RegistrationOfDetentionDto registrationOfDetention)
        {
            new PrisonerServiceClient()
                .Execute(client => client.RegisterDetention(registrationOfDetention));
        }

        public IReadOnlyList<PrisonerDto> FindPrisonersByName(string search)
        {
            return new PrisonerServiceClient()
                .Execute(clinet => clinet.FindPrisonersByName(search));
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
    }
}

