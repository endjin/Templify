namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IPackageTokeniser))]
    public class PackageTokeniser : IPackageTokeniser
    {
        private readonly IBinaryFileFilter binaryFileFilter;
        private readonly IFileContentProcessor fileContentProcessor;
        private readonly IProgressNotifier progressNotifier;
        private readonly IRenameFileProcessor renameFileProcessor;

        [ImportingConstructor]
        public PackageTokeniser(
            IFileContentProcessor fileContentProcessor, 
            IProgressNotifier progressNotifier, 
            IRenameFileProcessor renameFileProcessor, 
            IBinaryFileFilter binaryFileFilter)
        {
            this.fileContentProcessor = fileContentProcessor;
            this.binaryFileFilter = binaryFileFilter;
            this.progressNotifier = progressNotifier;
            this.renameFileProcessor = renameFileProcessor;
        }

        public Package Tokenise(Package package, string token)
        {
            this.TokeniseDirectoriesAndFiles(package, token);
            this.TokeniseFileContent(package, token);

            return package;
        }

        private static string Replace(string token, string value)
        {
            return Regex.Replace(value, token, match => Tokens.TokenName, RegexOptions.IgnoreCase);
        }

        private void TokeniseFileContent(Package package, string token)
        {
            int progress = 0;
            int fileCount = package.Manifest.Files.Count;

            var processableFiles = this.binaryFileFilter.Filter(package.Manifest.Files);

            Parallel.ForEach(
                processableFiles,
                manifestFile =>
                    {
                        var contents = this.fileContentProcessor.ReadContents(manifestFile.File);
                        contents = Replace(token, contents);

                        this.fileContentProcessor.WriteContents(manifestFile.File, contents);
                        this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageContents, fileCount, progress);

                        progress++;
                    });
        }

        private void TokeniseDirectoriesAndFiles(Package package, string token)
        {
            int progress = 0;
            int fileCount = package.Manifest.Files.Count;

            Parallel.ForEach(
                package.Manifest.Files,
                manifestFile =>
                    {
                        var tokenisedName = Replace(token, manifestFile.File);
                        tokenisedName = this.RebaseToTemplatePath(package, tokenisedName);
                        this.renameFileProcessor.Process(manifestFile.File, tokenisedName);
                        manifestFile.File = tokenisedName;
                        this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageStructure, fileCount, progress);
                        progress++;
                    });
        }

        private string RebaseToTemplatePath(Package package, string tokenisedName)
        {
            return tokenisedName.Replace(package.ClonedPath, package.TemplatePath);
        }
    }
}