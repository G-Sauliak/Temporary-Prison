using log4net;
using System;
using System.ServiceModel;
using Temporary_Prison.Data.UserService;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Clients
{
    public class UserClient : IUserClient
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public UserDto GetUserByName(string userName)
        {
            var userClient = new UserServiceClient();
            UserDto userDto = null;

            try
            {
                userClient.Open();

                userDto = userClient.GetUserByName(userName);

                if (userDto == null)
                {
                    log.Error("UserDto. Client returned null");
                    return default(UserDto);
                    //TODO

                }

                userClient.Close();
            }
            catch (FaultException<DataErrorDto> ex)
            {
                //TODO
            }
            finally
            {
                if (userClient.State == CommunicationState.Faulted)
                {
                    userClient.Abort();
                }
                else
                {
                    userClient.Close();
                }
            }
            return userDto;
        }

        public bool IsValidLogin(string userName, string password)
        {
            var userClient = new UserServiceClient();
            bool result = false;

            try
            {
                userClient.Open();

                result = userClient.IsValidLogin(userName, password);

                if (!result)
                {
                    log.Error("Login is not valid");
                    return default(bool);
                    //TODO

                }

                userClient.Close();
            }
            catch (FaultException<DataErrorDto> ex)
            {
                //TODO
            }
            finally
            {
                if (userClient.State == CommunicationState.Faulted)
                {
                    userClient.Abort();
                }
                else
                {
                    userClient.Close();
                }
            }
            return result;
        }
    }
}
