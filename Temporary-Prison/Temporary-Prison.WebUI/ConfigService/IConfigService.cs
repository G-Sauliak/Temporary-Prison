namespace Temporary_Prison.WebUI.SiteConfigService
{
    public interface IConfigService
    {
        string PhotoPath { get; set; }
        string AvatarPath { get; set; }
        string DefaultPhotoOfPrisoner { get; set; }
        int AvatarHeight { get; set; }
        int AvatarWidth { get; set; }
        int PhotoHeight { get; set; }
        int PhotoWidth { get; set; }
        int ImageMaxSize { get; set; }
        string AllowedPhotoTypes { get; set; }
        int PrisonerPagedSize { get; set; }
        int UserPagedSize { get; set; }
        string ContentPath { get; set; }
        string DefaultNoAvatar { get; set; }
    }
}