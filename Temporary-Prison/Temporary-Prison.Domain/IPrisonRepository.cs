using System.Collections.Generic;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Domain.Repositories
{
    public interface IPrisonRepository
    {
        IReadOnlyList<Prisoner> GetPrisoners();
    }
}
