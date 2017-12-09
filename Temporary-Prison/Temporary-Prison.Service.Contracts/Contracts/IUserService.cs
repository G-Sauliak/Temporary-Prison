﻿using System.ServiceModel;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    interface IUserService
    {
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool IsValidLogin(string userName, string password);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        UserDto GetUserByName(string userName);

    }
}
