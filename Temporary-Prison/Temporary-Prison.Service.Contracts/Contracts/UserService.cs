using log4net;
using System;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repository;


namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class UserService : IUserService
    {
        private readonly ILog log = LogManager.GetLogger("LOGGER");

        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public string[] GetRoles(string userName)
        {
            string[] userRoles = null;
            try
            {
                userRoles = userRepository.GetRoles(userName);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = "Error. GetRoles";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return userRoles;
        }

        public UserDto GetUserByName(string userName)
        {
            UserDto userDto = null;
            try
            {
                userDto = userRepository.GetUserByName(userName);
                userDto.Roles = userRepository.GetRoles(userName);
            }

            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = "Error. GetUserByName";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return userDto;
        }

        public bool IsValidLogin(string userName, string password)
        {
            bool result = false;
            try
            {
                result = userRepository.IsValidUser(userName, password);
            }

            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = "Error. IsValidLogin";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return result;
        }


    }
}
