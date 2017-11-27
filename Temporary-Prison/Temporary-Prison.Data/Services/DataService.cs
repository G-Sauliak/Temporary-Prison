using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.Converters;


namespace Temporary_Prison.Data.Services
{
    public class DataService : IDataService
    {
        private readonly IPrisonClient prisonClient;
        private readonly IPrisonServiceConvert prisonServiceConvert;
   

        public DataService(IPrisonClient prisonClient, IPrisonServiceConvert convertPrisoners)
        {
            this.prisonClient = prisonClient;
            this.prisonServiceConvert = convertPrisoners;
  
        }

        IReadOnlyList<Prisoner> IDataService.GetPrisoners()
        {
            var prisonerDto = prisonClient.GetPrisoners();

            if (prisonerDto != null)
            {
                return prisonServiceConvert.ToListPrisoners(prisonerDto);
            }

            return new List<Prisoner>() { new Prisoner { FirstName = "oops TODO..." } };
        }
    }
}
