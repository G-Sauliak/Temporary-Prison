using log4net;
using System;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public static class UsingExtensionClient
    {
        private static readonly ILog log = LogManager.GetLogger("LOGGER");

        public static TResult Execute<TServiceClient, TResult>(this TServiceClient client, Func<TServiceClient, TResult> clientFunction)
            where TServiceClient : ICommunicationObject
        {
            try
            {
                log.Info($"ServiceClent: {typeof(TServiceClient)}, GetResponce: {typeof(TResult)} ");

                var result = clientFunction(client);

                client.Close();

                return result;
            }
            catch (FaultException<DataErrorDto> e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (CommunicationException e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (TimeoutException e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                client.Abort();
                throw;
            }
            return default(TResult);
        }

        public static void Execute<TServiceClient>(this TServiceClient client, Action<TServiceClient> clientMethod)
           where TServiceClient : ICommunicationObject
        {
            try
            {
                log.Info($"ServiceClent: {typeof(TServiceClient)}");

                clientMethod(client);

                client.Close();
            }
            catch (FaultException<DataErrorDto> e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (CommunicationException e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (TimeoutException e)
            {
                log.Error(e.Message);
                client.Abort();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                client.Abort();
                throw;
            }
        }
    }
}
