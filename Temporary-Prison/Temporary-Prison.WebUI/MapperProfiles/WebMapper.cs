using AutoMapper;
using Temporary_Prison.Models;
using Temporary_Prison.Common.Models;
using Temporary_Prison.Business.SiteConfigService;
using System.IO;

namespace Temporary_Prison.WebMapperProfile
{
    public class WebMapper : Profile
    {
        private readonly IConfigService siteConfigService;

        public WebMapper() : this(new ConfigService())
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
               .ForMember(x => x.Photo, opt => opt.MapFrom(src =>
               (!string.IsNullOrEmpty(src.Photo))
               ? Path.Combine($"/{siteConfigService.ContentPath}/", $"{siteConfigService.PhotoPath}/", src.Photo)
               : Path.Combine($"/{siteConfigService.ContentPath}/", $"{siteConfigService.PhotoPath}/",
               siteConfigService.DefaultPhotoOfPrisonerPath)
               ));

            CreateMap<Prisoner, PrisonerPagedListViewModel>()
               .ForMember(x => x.Avatar, opt => opt.MapFrom(src =>
               (!string.IsNullOrEmpty(src.Photo))
               ? Path.Combine($"/{siteConfigService.ContentPath}/", $"{siteConfigService.AvatarPath}/", src.Photo)
               : Path.Combine($"/{siteConfigService.ContentPath}/", $"{siteConfigService.AvatarPath}/", 
               siteConfigService.DefaultNoAvatar)
               ));

            CreateMap<Detention, DetailsOfDetentionViewModel>();
            CreateMap<DetentionPagedList, DetentionPagedListViewModel>();
            CreateMap<EditDetentionViewModel, Detention>();
            CreateMap<ReleaseOfPrisoner, ReleaseOfPrisonerViewModel>();
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<Prisoner, CreateOrUpdatePrisonerViewModel>()
               .ForMember(x => x.Photo, opt => opt.MapFrom(src =>
               (!string.IsNullOrEmpty(src.Photo))
               ? Path.Combine($"/{siteConfigService.ContentPath}/", $"{siteConfigService.PhotoPath}/", src.Photo)
               : default(string)
               ));
            #endregion
        }
        public WebMapper(IConfigService siteConfigService)
        {
            this.siteConfigService = siteConfigService;
        }

    }

}