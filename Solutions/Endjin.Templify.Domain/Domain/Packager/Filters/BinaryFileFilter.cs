namespace Endjin.Templify.Domain.Domain.Packager.Filters
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IBinaryFileFilter))]
    public class BinaryFileFilter : IBinaryFileFilter
    {
        private readonly List<string> fileExclusions = new List<string>();

        public BinaryFileFilter()
        {
            this.fileExclusions = new List<string> { ".exe", ".dll", ".jpg", ".png", ".gif", ".snk" };
        }

        public List<ManifestFile> Filter(List<ManifestFile> files)
        {
            return files.Where(file => !this.fileExclusions.Contains(new FileInfo(file.File).Extension.ToLowerInvariant())).ToList();
        }
    }
}