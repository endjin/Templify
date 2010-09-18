namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageTokeniser))]
    public class PackageTokeniser : IPackageTokeniser
    {
        private readonly IRenameFileProcessor renameFileProcessor;
        private readonly IFileContentProcessor fileContentProcessor;

        [ImportingConstructor]
        public PackageTokeniser(IFileContentProcessor fileContentProcessor, IRenameFileProcessor renameFileProcessor)
        {
            this.fileContentProcessor = fileContentProcessor;
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
            return Regex.Replace(value, token, match => "__NAME__", RegexOptions.IgnoreCase);
        }

        private void TokeniseFileContent(Package package, string token)
        {
            foreach (var manifestFile in package.Manifest.Files)
            {
                var contents = this.fileContentProcessor.ReadContents(manifestFile.File);
                contents = Replace(token, contents);
                this.fileContentProcessor.WriteContents(manifestFile.File, contents);
            }
        }

        private void TokeniseDirectoriesAndFiles(Package package, string token)
        {
            foreach (var manifestFile in package.Manifest.Files)
            {
                var tokenisedName = Replace(token, manifestFile.File);
                tokenisedName = this.RebaseToTemplatePath(package, tokenisedName);
                this.renameFileProcessor.Process(manifestFile.File, tokenisedName);
                manifestFile.File = tokenisedName;
            }
        }

        private string RebaseToTemplatePath(Package package, string tokenisedName)
        {
            return tokenisedName.Replace(package.ClonedPath, package.TemplatePath);
        }
    }
}