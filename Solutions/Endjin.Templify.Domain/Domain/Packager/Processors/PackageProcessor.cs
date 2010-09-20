namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IPackageProcessor))]
    public class PackageProcessor : IPackageProcessor
    {
        #region Fields

        private readonly IArtefactProcessor artefactProcessor;
        private readonly IBinaryFileFilter binaryFileFilter;
        private readonly ICleanUpProcessor cleanUpProcessor;
        private readonly IProgressNotifier progressNotifier;
        private readonly ITemplateTokeniser templateTokeniser;

        #endregion

        [ImportingConstructor]
        public PackageProcessor(
            [Import("FilteredFileSystemArtefactProcessor")]IArtefactProcessor artefactProcessor,
            ICleanUpProcessor cleanUpProcessor,
            IProgressNotifier progressNotifier,
            ITemplateTokeniser templateTokeniser, 
            IBinaryFileFilter binaryFileFilter)
        {
            this.artefactProcessor = artefactProcessor;
            this.binaryFileFilter = binaryFileFilter;
            this.cleanUpProcessor = cleanUpProcessor;
            this.progressNotifier = progressNotifier;
            this.templateTokeniser = templateTokeniser;
        }

        public void Process(string path, string name)
        {
            this.ProcessFiles(path, name);
            this.ProcessDirectories(path);
        }

        private void ProcessDirectories(string path)
        {
            var directories = this.artefactProcessor.RetrieveDirectories(path).ToList();
            int progress = 0;
            int fileCount = directories.Count();

            foreach (var directory in directories)
            {
                if (directory.Contains(Tokens.TokenName))
                {
                    this.cleanUpProcessor.Process(directory);
                }

                this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageContents, fileCount, progress);
                progress++;
            }
        }

        private void ProcessFiles(string path, string name)
        {
            var files = this.artefactProcessor.RetrieveFiles(path);
            
            this.ProcessFileContents(files, name);
            this.ProcessDirectoryAndFilePaths(files, name);
        }

        private void ProcessDirectoryAndFilePaths(IEnumerable<string> files, string name)
        {
            int fileCount = files.Count();
            int progress = 0;

            foreach (var file in files)
            {
                this.templateTokeniser.TokeniseDirectoryAndFilePaths(file, name);
                this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageStructure, fileCount, progress);
                progress++;
            }
        }

        private void ProcessFileContents(IEnumerable<string> files, string name)
        {
            var filteredFiles = this.binaryFileFilter.Filter(files);

            int progress = 0;
            int fileCount = filteredFiles.Count();

            foreach (var file in filteredFiles)
            {
                this.templateTokeniser.TokeniseFileContent(file, name);
                this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageContents, fileCount, progress);
                progress++;
            }
        }
    }
}