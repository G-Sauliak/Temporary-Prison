using System.Collections.Generic;
using Temporary_Prison.Business.PrisonServices;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Domain.Repositories;

namespace Temporary_Prison.Business
{
    public class PrisonProvider : IPrisonProvider
    {
        private readonly IPrisonerRepository prisonerRepository;

        public PrisonProvider(IPrisonerRepository prisonerRepository)
        {
            this.prisonerRepository = prisonerRepository;
        }

        public IReadOnlyList<PrisonerProfile> GetPrisoner()
        {
         
            return prisonerRepository.GetPrisoners();
        }
    }
}
