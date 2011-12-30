namespace Endjin.Templify.Domain.Domain.Packager.Tokeniser
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;

    #endregion

    [Export(typeof(ITemplateTokeniser))]
    public class TemplateTokeniser : ITemplateTokeniser
    {
        private readonly IFileContentProcessor fileContentProcessor;
        private readonly IEnumerable<IFunctionTokenizer> functionTokenizers;
        private readonly IRenameFileProcessor renameFileProcessor;

        [ImportingConstructor]
		public TemplateTokeniser(IRenameFileProcessor renameFileProcessor, IFileContentProcessor fileContentProcessor, [ImportMany] IEnumerable<IFunctionTokenizer> FunctionTokenizers)
        {
            this.renameFileProcessor = renameFileProcessor;
            this.fileContentProcessor = fileContentProcessor;
            this.functionTokenizers = FunctionTokenizers;
        }

        public void TokeniseDirectoryAndFilePaths(string file, Dictionary<string, string> tokens)
        {
            var tokenisedName = Replace(tokens, file);
            this.renameFileProcessor.Process(file, tokenisedName);
        }

        public void TokeniseFileContent(string file, Dictionary<string, string> tokens)
        {
            var contents = this.fileContentProcessor.ReadContents(file);
            contents = Replace(tokens, contents);
            this.fileContentProcessor.WriteContents(file, contents);
        }

		private string Replace(Dictionary<string, string> tokens, string value)
        {
            string val = tokens.Aggregate(value, (current, token) => Regex.Replace(current, token.Key, match => token.Value));
            if ( this.functionTokenizers != null ) {
                val = this.functionTokenizers.Aggregate(val, (current, tokenizer) => tokenizer.TokenizeContent(current));
            }
            return val;
        }
    }
}