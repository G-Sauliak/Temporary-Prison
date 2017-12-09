using log4net;
using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class PrisonerClient : IPrisonerClient
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerDto GetPrisonerById(int Id)
        {
            var client = new PrisonerServiceClient();

            PrisonerDto prisoner = null;

            try
            {
                client.Open();

                prisoner = client.GetPrisonerById(Id);

                if (prisoner == null)
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
            return prisoner;
        }

        public IReadOnlyList<PrisonerDto> GetPrisoners()
        {
            var client = new PrisonerServiceClient();

            IReadOnlyList<PrisonerDto> prisoners = null;

            try
            {
                client.Open();

                prisoners = client.GetPrisoners();

                if (prisoners == null)
                {
                    log.Error("client getPriosners returned null");
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
            return prisoners;
        }
    }
}
