namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.IO;

    #endregion

    public static class FilePaths
    {
        public static string AppUserDataPath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"Endjin\Templify\");
            }
        }

        public static string ConfigurationDirectory
        {
            get { return Path.Combine(AppUserDataPath, @"config\"); }
        }

        public static string ErrorLogs
        {
            get { return Path.Combine(AppUserDataPath, @"errors\"); }
        }

        public static string PackageRepository 
        { 
            get { return Path.Combine(AppUserDataPath, @"repo\"); }
        }

        public static string TemporaryPackageRepository
        {
            get { return Path.Combine(AppUserDataPath, @"tmp-repo\"); }
        }
    }
}