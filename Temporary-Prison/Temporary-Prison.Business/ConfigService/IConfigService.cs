namespace Temporary_Prison.Business.SiteConfigService
{
    public interface IConfigService
    {
        string PrisonerPhotoPath { get; set; }
        string DefaultPhotoOfPrisonerPath { get; set; }
        int PhotoHeight { get; set; }
        int PhotoWidth { get; set; }
        int MaxPhotoSize { get; set; }
        string AllowedPhotoTypes { get; set; }
        int PrisonerPagedSize { get; set; }
        int UserPagedSize { get; set; }
        string ContentPath { get; set; }
        string DefaultNoAvatar { get; set; }
    }
}