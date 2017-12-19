using AutoMapper;
using System.Threading.Tasks;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Data.PrisonService;
using Temporary_Prison.Data.UserService;

namespace Temporary_Prison.Data.MapperProfile
{
    public class DataMapper : Profile
    {
        public DataMapper()
        {
            CreateMap<Prisoner,PrisonerDto>();
            CreateMap<Task<Prisoner>, Task<PrisonerDto>>();
            CreateMap<User, UserDto>();
            CreateMap<PrisonerDto, Prisoner>();
            CreateMap<UserDto, User>();
        }
    }
}
