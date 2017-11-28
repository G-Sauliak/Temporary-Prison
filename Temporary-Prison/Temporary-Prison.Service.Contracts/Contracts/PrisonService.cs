using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repository;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonService : IPrisonService
    {
        private readonly PrisonDBContext context;
        private readonly DataErrorDto serviceData;

        public PrisonService()
        {
          
            context = new PrisonDBContext();
            serviceData = new DataErrorDto();
        }

        public List<PrisonerDto> GetPrisonerById()
        {
            throw new System.NotImplementedException();
        }

        public List<PrisonerDto> GetPrisoners()
        {
            List<PrisonerDto> prisoners = null;
            try
            {
                prisoners = context.GetPrisoners();
            }
            catch (SqlException ex)
            {
                serviceData.ErrorMessage = "Sql Exception";
                serviceData.ErrorDetails = ex.ToString();
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }

            catch (Exception ex)
            {
                serviceData.ErrorMessage = "Common exception.";
                serviceData.ErrorDetails = ex.ToString();
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }
            return prisoners;
        }
    }
}
