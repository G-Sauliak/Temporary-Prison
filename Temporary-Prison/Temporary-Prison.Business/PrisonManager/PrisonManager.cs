using Temporary_Prison.Common.Models;
using System.Web;
using Temporary_Prison.Data.Services;
using Temporary_Prison.Business.Providers;

namespace Temporary_Prison.Business.PrisonManager
{
    public class PrisonManager : IPrisonManager
    {
        private readonly IPrisonerDataService prisonerDataService;
        private readonly IPrisonerProvider prisonerProvider;

        public PrisonManager() : this( new PrisonerDataService(), new PrisonerProvider())
        { }

        public PrisonManager(IPrisonerDataService prisonerDataService,
            IPrisonerProvider prisonerProvider)
        {
            this.prisonerDataService = prisonerDataService;
            this.prisonerProvider = prisonerProvider;
        }
        public void AddPrisoner(Prisoner prisoner)
        {
            prisonerDataService.AddPrisoner(prisoner);
        }

        public void DeleteDetention(int id)
        {
            prisonerDataService.DeleteDetention(id);

            var cacheKey = $"Detention_{id}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void DeletePrisoner(int id)
        {
            prisonerDataService.DeletePrisoner(id);

            var cacheKey = $"prisoner_{id}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void EditDetention(Detention detention)
        {
            prisonerDataService.EditDetention(detention);

            var cacheKey = $"Detention_{detention.DetentionID}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void EditPrisoner(Prisoner updatedPrisoner)
        {
            prisonerDataService.EditPrisoner(updatedPrisoner);

            var cacheKey = $"prisoner_{updatedPrisoner.PrisonerId}";
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public void RegisterDetention(RegistDetention registDetention)
        {
            prisonerDataService.RegisterDetention(registDetention);
        }

        public void ReleaseOfPrisoner(ReleaseOfPrisoner release)
        {
            prisonerDataService.ReleaseOfPrisoner(release);
            var cacheKey = $"Detention_{release.DetentionID}";
            HttpRuntime.Cache.Remove(cacheKey);
        }
    }
}
