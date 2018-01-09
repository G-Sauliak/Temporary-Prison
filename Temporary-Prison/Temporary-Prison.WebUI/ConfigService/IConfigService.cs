namespace Temporary_Prison.SiteConfigService
{
    public interface IConfigService
    {
        string PhotoImagePath { get; set; }
        int PhotoHeight { get; set; }
        int PhotoWidth { get; set; }
        int MaxPhotoSize { get; set; }
        string AllowedPhotoTypes { get; set; }
    }
}