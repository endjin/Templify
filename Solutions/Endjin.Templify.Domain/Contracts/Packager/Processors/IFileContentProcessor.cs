namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    public interface IFileContentProcessor
    {
        string ReadContents(string path);

        void WriteContents(string path, string content);
    }
}