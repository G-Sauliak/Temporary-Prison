using AutoMapper;
using Temporary_Prison.Models;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.WebMapperProfile
{
    public partial class WebMapper : Profile
    {
        public WebMapper()
        {
            #region Map for admin
            CreateMap<User, EditUserViewModel>()
                .ForMember(p => p.UserAndRoles, opt => opt.Ignore());

            CreateMap<EditUserViewModel, User>()
              .ForMember(p => p.Roles, opt => opt.Ignore());
            CreateMap<UserAndRoles, UserAndRolesViewModel>();
            #endregion

            #region Map for prisoner
            CreateMap<Prisoner, DetailsPrisonerViewModel>()
                 .ForMember(p => p.DetentionPagedList, opt => opt.Ignore())
                 .ForMember(x => x.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToShortDateString()));

            CreateMap<Prisoner, PrisonerPagedListViewModel>()
               .ForMember(x => x.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToShortDateString()));
            CreateMap<Detention, DetailsOfDetentionViewModel>()
                .ForMember(x => x.DateOfArrival, opt => opt.MapFrom(src => src.DateOfArrival.HasValue ? src.DateOfArrival.Value.ToShortDateString() : null))
                .ForMember(x => x.DateOfDetention, opt => opt.MapFrom(src => src.DateOfDetention.HasValue ? src.DateOfDetention.Value.ToShortDateString() : null))
                .ForMember(x => x.DateOfRelease, opt => opt.MapFrom(src => src.DateOfRelease.HasValue ? src.DateOfRelease.Value.ToShortDateString() : null));

            CreateMap<DetentionPagedList, DetentionPagedListViewModel>()
                .ForMember(x => x.DateOfDetention, opt => opt.MapFrom(src => src.DateOfDetention.HasValue ? src.DateOfDetention.Value.ToShortDateString() : string.Empty))
                .ForMember(x => x.DateOfRelease, opt => opt.MapFrom(src => src.DateOfRelease.HasValue ? src.DateOfRelease.Value.ToShortDateString() : string.Empty));

            CreateMap<EditDetentionViewModel, Detention>();
            CreateMap<ReleaseOfPrisoner, ReleaseOfPrisonerViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Prisoner, CreateOrUpdatePrisonerViewModel>();
            #endregion
        }
    }

}