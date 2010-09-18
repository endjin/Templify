namespace Endjin.Templify.Domain.Domain.Packager.Specifications
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Specifications;
    using Endjin.Templify.Domain.Framework.Specifications;

    #endregion;

    [Export(typeof(IFileExclusionsSpecification))]
    public class FileExclusionSpecification : QuerySpecification<string>, IFileExclusionsSpecification
    {
        private readonly List<string> directoryExclusions = new List<string>();
        private readonly List<string> fileExclusions = new List<string>();

        public FileExclusionSpecification()
        {
            this.directoryExclusions = new List<string> { "bin", "obj", "debug", "release" };
            this.fileExclusions = new List<string> { ".exe", ".cache", ".dll", ".pdb", ".jpg", ".png", ".gif", ".mst", ".msi", ".msm", ".gitignore", ".idx", ".pack", ".user", ".suo" };
        }

        public override System.Linq.Expressions.Expression<System.Func<string, bool>> MatchingCriteria
        {
            get 
            { 
                return f =>
                    (!this.directoryExclusions.Contains(new FileInfo(f).Directory.Name.ToLowerInvariant())) &&
                    (!this.fileExclusions.Contains(new FileInfo(f).Extension.ToLowerInvariant())); 
            }
        }
    }
}