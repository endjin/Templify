namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

    #endregion

    [Export(typeof(ITemplateTokeniser))]
    public class TemplateTokeniser : ITemplateTokeniser
    {
        private readonly IRenameFileProcessor renameFileProcessor;
        private readonly IFileContentProcessor fileContentProcessor;

        [ImportingConstructor]
        public TemplateTokeniser(IRenameFileProcessor renameFileProcessor, IFileContentProcessor fileContentProcessor)
        {
            this.renameFileProcessor = renameFileProcessor;
            this.fileContentProcessor = fileContentProcessor;
        }

        public void Tokenise(string file, string token)
        {
            this.TokeniseFileContent(file, token);
            this.TokeniseDirectoriesAndFiles(file, token);
        }

        private static string Replace(string token, string value)
        {
            return Regex.Replace(value, "__NAME__", match => token);
        }

        private void TokeniseFileContent(string file, string token)
        {
            var contents = this.fileContentProcessor.ReadContents(file);
            contents = Replace(token, contents);
            this.fileContentProcessor.WriteContents(file, contents);
        }

        private void TokeniseDirectoriesAndFiles(string file, string token)
        {
            var tokenisedName = Replace(token, file);
            this.renameFileProcessor.Process(file, tokenisedName);
        }
    }
}