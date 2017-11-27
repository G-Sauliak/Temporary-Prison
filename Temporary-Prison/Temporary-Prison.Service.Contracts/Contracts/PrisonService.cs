using System.Collections.Generic;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repository;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonService : IPrisonService
    {
        private readonly PrisonDBContext context;

        public PrisonService()
        {
            context = new PrisonDBContext();
        }

        public List<PrisonerDto> GetPrisonerById()
        {
            throw new System.NotImplementedException();
        }

        public List<PrisonerDto> GetPrisoners()
        {
            return context.GetPrisoners();
        }
    }
}
