using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Business
{
    public interface IPrisonProvider
    {
        IReadOnlyList<PrisonerProfile> GetPrisoner();
    }
}
