using AutoMapper;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Models;

namespace Temporary_Prison.MapperProfile
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<Prisoner, CreatePrisonerViewModel>();
            CreateMap<User, EditUserViewModel>()
                .ForMember(p => p.UserAndRoles, opt => opt.Ignore());
            CreateMap<EditUserViewModel, User>()
              .ForMember(p => p.Roles, opt => opt.Ignore());
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Prisoner, DetailsPrisonerViewModel>();
        }
    }

}