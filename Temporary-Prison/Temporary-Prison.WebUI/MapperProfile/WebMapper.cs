using AutoMapper;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Models;

namespace Temporary_Prison.MapperProfile
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<Prisoner, AddPrisonerViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }

}