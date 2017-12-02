using log4net;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repository;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonService : IPrisonService
    {

        private readonly ILog log = LogManager.GetLogger("LOGGER");

        private readonly PrisonRepository context;
        private readonly DataErrorDto serviceData;

        public PrisonService()
        {
          
            context = new PrisonRepository();
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
            catch (DbException ex)
            {
                serviceData.ErrorMessage = "Error DbExceprion. GetPriosners";
                serviceData.ErrorDetails = ex.ToString();
                log.Error($"Info Error: {serviceData.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }

            catch (Exception ex)
            {
                serviceData.ErrorMessage = "Error Exceprion. GetPriosners";
                serviceData.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceData.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }
            return prisoners;
        }
    }
}
