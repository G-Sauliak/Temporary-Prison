using log4net;
using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonClient : IPrisonClient
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
            var client = new PrisonServiceClient();

            IReadOnlyList<PrisonerDto> result = null;

            try
            {
                client.Open();

                result = client.GetPrisoners();

                if (result == null)
                {
                    log.Error("client returned null");
                    //TODO
                 
                }

                client.Close();
            }
            catch (FaultException<DataErrorDto> ex)
            {
              //TODO
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
