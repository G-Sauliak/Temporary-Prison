
using System.Collections.Generic;
using Temporary_Prison.Common.Models;

using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.Converters
{
    public interface IConvertPrisoners
    {
        IReadOnlyList<PrisonerProfile> ToListPrisonProfile(IReadOnlyList<PrisonerProfileDto> prisoners);
    }
}
