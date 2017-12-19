﻿using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    [ServiceContract]
    public interface IPrisonerService
    {
        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        List<PrisonerDto> GetPrisoners();

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        List<PrisonerDto> GetPrisonersForPagedList(int skip, int rowSize, out int totalCount);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto GetPrisonerById(int Id);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        bool AddPrisoner(PrisonerDto prisoner, out int newId);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        PrisonerDto[] FindPrisonersByName(string search);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void EditPrisoner(PrisonerDto prisoner);

        [OperationContract]
        [FaultContract(typeof(DataErrorDto))]
        void DeletePrisoner(int id);
    }
}
