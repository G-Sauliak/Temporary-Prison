using System.Collections.Generic;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonClient : IPrisonClient
    {
      
        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
            IReadOnlyList<PrisonerDto> result = null;
            using (var client = new PrisonServiceClient())
            {
                client.Open();

                result = client.GetPrisoners();

                client.Close();
            }
            return result;
        }
    }
}
