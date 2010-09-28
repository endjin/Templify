namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Specifications;

    #endregion

    [Export("FilteredFileSystemArtefactProcessor", typeof(IArtefactProcessor))]
    public class FilteredFileSystemArtefactProcessor : FileSystemArtefactProcessor
    {
        private readonly IFileExclusionsSpecification fileExclusionsSpecification;
        private readonly IConfiguration configuration;

        [ImportingConstructor]
        public FilteredFileSystemArtefactProcessor(IFileExclusionsSpecification fileExclusionsSpecification, IConfiguration configuration)
        {
            this.fileExclusionsSpecification = fileExclusionsSpecification;
            this.configuration = configuration;
        }

        public override IEnumerable<string> RetrieveFiles(string path)
        {
            this.SetFilters();

            return this.fileExclusionsSpecification.SatisfyingElementsFrom(base.RetrieveFiles(path).AsQueryable());
        }

        private void SetFilters()
        {
            this.fileExclusionsSpecification.FileExclusions = this.ParseList(this.configuration.GetFileExclusions());
            this.fileExclusionsSpecification.DirectoryExclusions = this.ParseList(this.configuration.GetDirectoryExclusions());
        }

        private List<string> ParseList(string commaSeparatedString)
        {
            return commaSeparatedString.Split(";".ToCharArray()).ToList(); 
        }
    }
}