using System.Collections.Generic;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.Clients;
using Temporary_Prison.Data.Converters;


namespace Temporary_Prison.Data.Services
{
    public class DataService : IDataService
    {
        private readonly IPrisonClient prisonClient;
        private readonly IConvertPrisoners convertPrisoners;
   

        public DataService(IPrisonClient prisonClient, IConvertPrisoners convertPrisoners)
        {
            this.prisonClient = prisonClient;
            this.convertPrisoners = convertPrisoners;
  
        }

        IReadOnlyList<PrisonerProfile> IDataService.GetPrisoners()
        {
            var prisonersProfileDto = prisonClient.GetPrisoners();
            if (prisonersProfileDto != null)
            {
                return convertPrisoners.ToListPrisonProfile(prisonersProfileDto);
            }
            return new List<PrisonerProfile>() { new PrisonerProfile { FirstName = "default" } };
        }
    }
}
