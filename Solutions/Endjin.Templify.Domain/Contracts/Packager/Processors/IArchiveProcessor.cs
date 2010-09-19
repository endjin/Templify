namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    using System.IO;

    public interface IArchiveProcessor
    {
        void Extract(string archivePath, string filePath, string destFilePath);

        Stream Extract(string archivePath, string filePath);
    }
}