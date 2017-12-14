using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonerClient : IPrisonerClient
    {
        public PrisonerDto GetPrisonerById(int Id)
        {
            return new PrisonerServiceClient().GetResult(client => client.GetPrisonerById(Id));
        }

        public IReadOnlyList<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount)
        {
            int totalCountPrisoners = default(int);
         
            var prisoners = new PrisonerServiceClient().GetResult(client => client.GetPrisonersForPagedList(skip, rowSize, out totalCountPrisoners));

            totalCount = totalCountPrisoners;

            return prisoners;
        }

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
           return new PrisonerServiceClient().GetResult(client => client.GetPrisoners());
        }

        public bool TryAddPrisoner(PrisonerDto prisoner, out int newId)
        {
            var _newId = default(int);

            var result = new PrisonerServiceClient().GetResult(client => client.TryAddPrisoner(prisoner, out _newId));

            newId = _newId;

            return result;
        }
    }
}
