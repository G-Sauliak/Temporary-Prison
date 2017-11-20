using System.Collections.Generic;
using System.ServiceModel;

using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Service
{
    [ServiceContract]
    public interface IPrisonService
    {
        [OperationContract]
        IReadOnlyList<PrisonerProfile> GetPrisoner();
    }
}
