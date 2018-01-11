﻿namespace Temporary_Prison.Infrastructure
{
    public static class DefaultConfig
    {
        public const string DefaultAllowedPhotoTypes = "image/png;image/jpg;image/jpeg;";
        public const int DefaultMaxSize = 1024;
        public const string DefaultPhotoPath = "PhotosOfPrisoners";
        public const int DefaultPhotoHeight = 100;
        public const int DefaultPhotoWidth = 100;
        public const int PrisonerPagedSize = 5;
        public const int UserPagedSize = 5;
        public const string defaultPhotoOfPrisonerPath = "DefaultPhoto/defaultPhotoPrisoner.jpg";
    }
}