namespace Temporary_Prison.Business.Infrastructure
{
    public static class DefaultConfig
    {
        public const string DefaultAllowedPhotoTypes = "image/png;image/jpg;image/jpeg;";
        public const int DefaultMaxSize = 1024;
        public const string DefaultPhotoPath = "PhotosOfPrisoners";
        public const int DefaultAvatarHeight = 100;
        public const int DefaultAvatarWidth = 100;
        public const int PrisonerPagedSize = 5;
        public const int UserPagedSize = 5;
        public const string DefaultPhotoOfPrisonerPath = "defaultPhotoPrisoner.jpg";
        public const string DefaultNoAvatarPath = "no-avatar.jpg";
        public const string ContentPath = "Content";
    }
}