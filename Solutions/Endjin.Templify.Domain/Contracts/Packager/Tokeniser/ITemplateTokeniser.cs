namespace Endjin.Templify.Domain.Contracts.Packager.Tokeniser
{
    public interface ITemplateTokeniser
    {
        void TokeniseFileContent(string file, string token);

        void TokeniseDirectoryAndFilePaths(string file, string token);
    }
}