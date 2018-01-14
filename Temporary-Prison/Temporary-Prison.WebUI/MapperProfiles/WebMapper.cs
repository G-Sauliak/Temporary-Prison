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
            CreateMap<Prisoner, DetailsPrisonerViewModel>();
            CreateMap<Prisoner, PrisonerPagedListViewModel>();
            CreateMap<Detention, DetailsOfDetentionViewModel>();
            CreateMap<DetentionPagedList, DetentionPagedListViewModel>();
            CreateMap<EditDetentionViewModel, Detention>();
            CreateMap<ReleaseOfPrisoner, ReleaseOfPrisonerViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Prisoner, CreateOrUpdatePrisonerViewModel>();
            #endregion
        }
    }

}