namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    using System.Collections.Generic;

    public interface IArtefactProcessor
    {
        IEnumerable<string> RetrieveDirectories(string path);

        IEnumerable<string> RetrieveFiles(string path);

        IEnumerable<string> RetrieveFiles(string path, string filter);

        void RemoveFile(string path);
    }
}