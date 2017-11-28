using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonClient : IPrisonClient
    {

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
            var client = new PrisonServiceClient();
            IReadOnlyList<PrisonerDto> result = null;

            try
            {
                client.Open();

                result = client.GetPrisoners();

                client.Close();
            }
            catch (FaultException<DataErrorDto> ex)
            {

                
            }
            finally
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }
                else
                {
                    client.Close();
                }
            }
            //TODO
            return result;
        }
    }
}
