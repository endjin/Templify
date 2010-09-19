namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Linq;

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
        private readonly ICleanUpProcessor cleanUpProcessor;
        private readonly IProgressNotifier progressNotifier;
        private readonly ITemplateTokeniser templateTokeniser;

        #endregion

        [ImportingConstructor]
        public PackageProcessor(
            [Import("FilteredFileSystemArtefactProcessor")]IArtefactProcessor artefactProcessor,
            ICleanUpProcessor cleanUpProcessor,
            IProgressNotifier progressNotifier,
            ITemplateTokeniser templateTokeniser)
        {
            this.artefactProcessor = artefactProcessor;
            this.cleanUpProcessor = cleanUpProcessor;
            this.progressNotifier = progressNotifier;
            this.templateTokeniser = templateTokeniser;
        }

        public void Process(string path, string name)
        {
            var files = this.artefactProcessor.RetrieveFiles(path);

            int progress = 0;
            int fileCount = files.Count();

            foreach (var file in files)
            {
                this.templateTokeniser.Tokenise(file, name);
                this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageContents, fileCount, progress);
                progress++;
            }

            var directories = this.artefactProcessor.RetrieveDirectories(path).ToList();

            progress = 0;
            fileCount = directories.Count();

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
    }
}