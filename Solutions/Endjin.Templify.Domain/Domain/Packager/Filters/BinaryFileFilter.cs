namespace Endjin.Templify.Domain.Domain.Packager.Filters
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IBinaryFileFilter))]
    public class BinaryFileFilter : IBinaryFileFilter
    {
        private readonly IConfiguration configuration;
        private List<string> fileExclusions = new List<string>();

        [ImportingConstructor]
        public BinaryFileFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<ManifestFile> Filter(IEnumerable<ManifestFile> files)
        {
            this.SetFilters();
            return files.Where(file => !this.fileExclusions.Contains(new FileInfo(file.File).Extension.ToLowerInvariant())).ToList();
        }

        public IEnumerable<string> Filter(IEnumerable<string> files)
        {
            this.SetFilters();
            return files.Where(file => !this.fileExclusions.Contains(new FileInfo(file).Extension.ToLowerInvariant())).ToList();
        }

        private void SetFilters()
        {
            this.fileExclusions = this.ParseList(this.configuration.GetTokeniseFileExclusions());
        }

        private List<string> ParseList(string commaSeparatedString)
        {
            return commaSeparatedString.Split(";".ToCharArray()).ToList();
        }
    }
}