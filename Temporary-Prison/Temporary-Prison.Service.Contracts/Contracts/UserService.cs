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

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddUser(UserDto user)
        {
            try
            {
                userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = "Error. AddUser";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public string[] GetAllRoles()
        {
            string[] userDto = default(string[]);
            try
            {
                userDto = userRepository.GetAllRoles();
            }

            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = "Error. GetAllRoles";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return userDto;
        }

        public UserDto GetUserByName(string userName)
        {
            UserDto userDto = default(UserDto);
            try
            {
                userDto = userRepository.GetUserByName(userName);
            }

            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. GetUserByName {userName}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return userDto;
        }

        public UserDto[] GetUsersForPagedList(int skip, int rowSize, out int totalCountUsers)
        {
            var usersDto = default(UserDto[]);
            try
            {
                usersDto = userRepository.GetUsersForPagedList(skip, rowSize, out totalCountUsers);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. GetUsersForPagedList skip: {skip} countRows: {rowSize}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return usersDto;
        }

        public bool IsExistsByLogin(string userName)
        {
            try
            {
                return userRepository.IsExistsByLogin(userName);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. IsExistsByLogin {userName}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public bool IsExistsByEmail(string email)
        {
            try
            {
                return userRepository.IsExistsByEmail(email);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. IsExistsByEmail {email}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
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
                serviceDataError.ErrorMessage = $"Error. IsValidLogin {userName}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
            return result;
        }

        public void EditUser(UserDto user)
        {
            try
            {
                userRepository.EditUser(user);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. EditUser: {user.UserName}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }

        public void DeleteUser(string userName)
        {
            try
            {
                userRepository.DeleteUser(userName);
            }
            catch (Exception ex)
            {
                var serviceDataError = new DataErrorDto();
                serviceDataError.ErrorMessage = $"Error. DeleteUser: {userName}";
                serviceDataError.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceDataError.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceDataError, ex.ToString());
            }
        }
    }

}
