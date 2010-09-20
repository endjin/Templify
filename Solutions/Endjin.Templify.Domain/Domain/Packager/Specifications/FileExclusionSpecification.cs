using Endjin.Templify.Domain.Contracts.Infrastructure;

namespace Endjin.Templify.Domain.Domain.Packager.Specifications
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Specifications;
    using Endjin.Templify.Domain.Framework.Specifications;

    #endregion;

    [Export(typeof(IFileExclusionsSpecification))]
    public class FileExclusionSpecification : QuerySpecification<string>, IFileExclusionsSpecification
    {
        private readonly IConfiguration configuration;
        private readonly List<string> directoryExclusions = new List<string>();
        private readonly List<string> fileExclusions = new List<string>();

        [ImportingConstructor]
        public FileExclusionSpecification(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.directoryExclusions = this.configuration.GetDirectoryExclusions().Split(",".ToCharArray()).ToList();
            this.fileExclusions = this.configuration.GetFileExclusions().Split(",".ToCharArray()).ToList();
        }

        public override System.Linq.Expressions.Expression<System.Func<string, bool>> MatchingCriteria
        {
            get { return f => !this.ShouldExclude(f); }
        }

        private bool ShouldExclude(string path)
        {
            var segments = path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

            bool shouldExclude = segments.Any(directory => this.directoryExclusions.Contains(directory));

            if (!shouldExclude)
            {
                var file = segments[segments.Length - 1];

                shouldExclude = this.fileExclusions.Contains(new FileInfo(file).Extension.ToLowerInvariant());
            }

            return shouldExclude;
        }
    }
}