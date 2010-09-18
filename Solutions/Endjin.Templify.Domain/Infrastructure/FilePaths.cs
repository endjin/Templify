namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.IO;

    #endregion

    public static class FilePaths
    {
        public static string PackageRepository 
        { 
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"SharpArch\Pkg-Repo\");
            }
        }

        public static string TemporaryPackageRepository
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"SharpArch\Tmp-Pkg-Repo\");
            }
        }
    }
}