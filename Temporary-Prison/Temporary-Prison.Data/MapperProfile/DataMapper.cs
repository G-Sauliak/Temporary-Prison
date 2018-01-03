using AutoMapper;
using Temporary_Prison.Common.Entities;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Data.UserService;

namespace Temporary_Prison.Data.MapperProfile
{
    public class DataMapper : Profile
    {
        public DataMapper()
        {
            CreateMap<Prisoner, PrisonerDto>();
            CreateMap<User, UserDto>();
            CreateMap<RegistDetention, RegistrationOfDetentionDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<DetentionPagedList, DetentionPagedListDto>();
        }
    }
}
