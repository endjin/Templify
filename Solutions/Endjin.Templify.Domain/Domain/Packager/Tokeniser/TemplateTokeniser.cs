namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(ITemplateTokeniser))]
    public class TemplateTokeniser : ITemplateTokeniser
    {
        private readonly IFileContentProcessor fileContentProcessor;
        private readonly IRenameFileProcessor renameFileProcessor;

        [ImportingConstructor]
        public TemplateTokeniser(IRenameFileProcessor renameFileProcessor, IFileContentProcessor fileContentProcessor)
        {
            this.renameFileProcessor = renameFileProcessor;
            this.fileContentProcessor = fileContentProcessor;
        }

        private void TokeniseDirectoriesAndFiles(string file, List<PackageConfigurationData> tokens)
        {
            var tokenisedName = Replace(tokens, file);
            this.renameFileProcessor.Process(file, tokenisedName);
        }

        private void TokeniseFileContent(string file, List<PackageConfigurationData> tokens)
        {
            var contents = this.fileContentProcessor.ReadContents(file);
            contents = Replace(tokens, contents);
            this.fileContentProcessor.WriteContents(file, contents);
        }

        private static string Replace(List<PackageConfigurationData> tokens, string value)
        {
            string replaced = value;
            foreach (var token in tokens)
            {
                replaced = Regex.Replace(replaced, token.Token, match => token.Value);
            }

			// TODO: Probably need a more meaningful return value?
            return replaced;
        }

        #region ITemplateTokeniser Members

        void ITemplateTokeniser.TokeniseFileContent(string file, List<PackageConfigurationData> tokens)
        {
            throw new System.NotImplementedException();
        }

        void ITemplateTokeniser.TokeniseDirectoryAndFilePaths(string file, List<PackageConfigurationData> tokens)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}