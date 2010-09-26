namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Endjin.Templify.Domain.Contracts.Packager.Filters;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageTokeniser))]
    public class PackageTokeniser : IPackageTokeniser
    {
        #region Fields

        private readonly IBinaryFileFilter binaryFileFilter;
        private readonly IFileContentProcessor fileContentProcessor;
        private readonly IProgressNotifier progressNotifier;
        private readonly IRenameFileProcessor renameFileProcessor;

        #endregion

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

        public Package Tokenise(Package package, Dictionary<string, string> tokens)
        {
            this.TokeniseDirectoriesAndFiles(package, tokens);
            this.TokeniseFileContent(package, tokens);

            return package;
        }

        private static string Replace(Dictionary<string, string> tokens, string value)
        {
            string result = value;

            foreach (var token in tokens)
            {
                result = Regex.Replace(value, token.Key, match => token.Value, RegexOptions.IgnoreCase);
            }
            
            return result;
        }

        private void TokeniseFileContent(Package package, Dictionary<string, string> tokens)
        {
            int progress = 0;
            int fileCount = package.Manifest.Files.Count;

            var processableFiles = this.binaryFileFilter.Filter(package.Manifest.Files);

            Parallel.ForEach(
                processableFiles,
                manifestFile =>
                    {
                        var contents = this.fileContentProcessor.ReadContents(manifestFile.File);

                        contents = Replace(tokens, contents);

                        this.fileContentProcessor.WriteContents(manifestFile.File, contents);
                        this.progressNotifier.UpdateProgress(ProgressStage.TokenisePackageContents, fileCount, progress);

                        progress++;
                    });
        }

        private void TokeniseDirectoriesAndFiles(Package package, Dictionary<string, string> tokens)
        {
            int progress = 0;
            int fileCount = package.Manifest.Files.Count;

            Parallel.ForEach(
                package.Manifest.Files,
                manifestFile =>
                    {
                        var tokenisedName = Replace(tokens, manifestFile.File);
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