namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxSizeInMB = 1;
        public const int MaxSizeInByts = MaxSizeInMB * 1024 * 1024;
    }
}
