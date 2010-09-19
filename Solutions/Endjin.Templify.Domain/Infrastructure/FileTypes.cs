namespace Endjin.Templify.Domain.Infrastructure
{
    #region Using Directives

    using System;
    using System.IO;

    #endregion

    public static class FileTypes
    {
        public static string PackageWildcard
        { 
            get
            {
                return "*.pkg";
            }
        }

        public static string Package
        {
            get
            {
                return ".pkg";
            }
        }
    }
}