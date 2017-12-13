using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Temporary_Prison.Service.Contracts.Dto;
using Temporary_Prison.Service.Contracts.Repositories;

namespace Temporary_Prison.Service.Contracts.Contracts
{
    public class PrisonerService : IPrisonerService
    {
        private readonly IPrisonerRepository prisonerContext;

        private readonly ILog log = LogManager.GetLogger("LOGGER");

        public PrisonerService()
        {
            prisonerContext = new PrisonerRepository();
        }

        public PrisonerDto GetPrisonerById(int Id)
        {
            PrisonerDto prisoner = null;
            try
            {
                prisoner = prisonerContext.GetPrisonerById(Id);
            }
     
            catch (Exception ex)
            {
                var serviceData = new DataErrorDto();
                serviceData.ErrorMessage = "Error Exception. GetPriosnerById";
                serviceData.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceData.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }
            return prisoner;

        }

        public List<PrisonerDto> GetPrisoners()
        {
            IReadOnlyList<PrisonerDto> prisoners = null;
            try
            {
                prisoners = prisonerContext.GetPrisoners();
            }
   
            catch (Exception ex)
            {
                var serviceData = new DataErrorDto();
                serviceData.ErrorMessage = "Error Exception. GetPriosners";
                serviceData.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceData.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }
            return prisoners as List<PrisonerDto>;
        }

        public bool TryAddPrisoner(PrisonerDto prisoner, out int newId)
        {
            bool result = false;
            try
            {
                result = prisonerContext.TryAddPrisoner(prisoner, out newId);
            }

            catch (Exception ex)
            {
                var serviceData = new DataErrorDto();
                serviceData.ErrorMessage = "Error Exception. GetPriosners";
                serviceData.ErrorDetails = ex.ToString();
                log.Error($"Type Error: {serviceData.ErrorMessage}\n ErrorDetails {ex.ToString()}");
                throw new FaultException<DataErrorDto>(serviceData, ex.ToString());
            }
            return result;
        }
    }
}
