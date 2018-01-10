using System.Collections.Generic;

namespace Temporary_Prison.Models
{
    public class ListOfPrisonersViewModel
    {
        public SearchFilterModelView SearchfilterModel { get; private set; }

        public IEnumerable<PrisonerPagedListViewModel> PagedListOfPriosners { get; set; }

        public ListOfPrisonersViewModel()
        {
            SearchfilterModel = new SearchFilterModelView();
        }
    }
}