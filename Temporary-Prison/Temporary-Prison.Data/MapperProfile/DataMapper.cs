using AutoMapper;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Service.Contracts.Dto;

namespace Temporary_Prison.Data.MapperProfile
{
    public class DataMapper : Profile
    {
        public DataMapper()
        {
            CreateMap<Prisoner, PrisonerDto>();
            CreateMap<User, UserDto>();
            CreateMap<PrisonerDto, Prisoner>();
        }
    }
}
