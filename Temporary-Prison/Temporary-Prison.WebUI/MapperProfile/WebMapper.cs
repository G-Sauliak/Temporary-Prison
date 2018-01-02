using AutoMapper;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Models;

namespace Temporary_Prison.MapperProfile
{
    public class WebMapper : Profile
    {
        public WebMapper()
        {
            CreateMap<Prisoner, PrisonerViewModel>();
            CreateMap<User, EditUserViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
        }
    }

}