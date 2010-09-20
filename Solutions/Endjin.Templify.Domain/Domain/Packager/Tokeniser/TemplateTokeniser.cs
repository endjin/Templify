namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
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

        public void TokeniseDirectoryAndFilePaths(string file, string token)
        {
            var tokenisedName = Replace(token, file);
            this.renameFileProcessor.Process(file, tokenisedName);
        }

        public void TokeniseFileContent(string file, string token)
        {
            var contents = this.fileContentProcessor.ReadContents(file);
            contents = Replace(token, contents);
            this.fileContentProcessor.WriteContents(file, contents);
        }

        private static string Replace(string token, string value)
        {
            return Regex.Replace(value, Tokens.TokenName, match => token);
        }
    }
}