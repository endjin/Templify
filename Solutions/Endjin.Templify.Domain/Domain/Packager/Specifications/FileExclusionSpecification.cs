namespace Endjin.Templify.Domain.Domain.Packager.Specifications
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Contracts.Packager.Specifications;
    using Endjin.Templify.Domain.Framework.Specifications;

    #endregion;

    [Export(typeof (IFileExclusionsSpecification))]
    public class FileExclusionSpecification : QuerySpecification<string>, IFileExclusionsSpecification
    {
        public override Expression<Func<string, bool>> MatchingCriteria
        {
            get { return f => !this.ShouldExclude(f); }
        }

        public List<string> FileExclusions { get; set; }

        public List<string> DirectoryExclusions { get; set; }

        private bool ShouldExclude(string path)
        {
            string[] segments = path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            bool shouldExclude = segments.Any(directory => this.DirectoryExclusions.Contains(directory));

            if (!shouldExclude)
            {
                string file = segments[segments.Length - 1];

                shouldExclude = this.FileExclusions.Contains(new FileInfo(file).Extension.ToLowerInvariant());
            }

            return shouldExclude;
        }
    }
}