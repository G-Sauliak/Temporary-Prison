using AutoMapper;
using Temporary_Prison.Models;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.MapperProfile
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<User, EditUserViewModel>()
                .ForMember(p => p.UserAndRoles, opt => opt.Ignore());

            CreateMap<EditUserViewModel, User>()
              .ForMember(p => p.Roles, opt => opt.Ignore());

            CreateMap<Prisoner, DetailsPrisonerViewModel>()
                 .ForMember(p => p.DetentionPagedList, opt => opt.Ignore());
            
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Prisoner, CreatePrisonerViewModel>();
        }
    }

}