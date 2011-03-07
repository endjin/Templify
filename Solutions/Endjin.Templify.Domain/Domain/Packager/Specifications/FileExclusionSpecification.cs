namespace Endjin.Templify.Domain.Domain.Packager.Specifications
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Specifications;
    using Endjin.Templify.Domain.Framework.Specifications;

    #endregion;

    [Export(typeof(IFileExclusionsSpecification))]
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

            bool shouldExclude = segments.Any(directory => this.DirectoryExclusions.Any(exclusion => Regex.IsMatch(directory, exclusion)));

            if (!shouldExclude)
            {
                string file = segments.Last();

                shouldExclude = this.FileExclusions.Any(exclusion => Regex.IsMatch(file, exclusion));
            }

            return shouldExclude;
        }
    }
}