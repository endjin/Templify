namespace Endjin.Templify.Domain.Domain.Packages
{
    #region Using Directives

    using System.Collections.Generic;

    using Endjin.Templify.Domain.Contracts.Packages;

    #endregion

    public class PackageMetaData : IPackageMetaData
    {
        public string Author { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public List<string> Tokens { get; set; }
    }
}