namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    #endregion

    [Export(typeof(IFileContentProcessor))]
    public class FileContentProcessor : IFileContentProcessor
    {
        public string ReadContents(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteContents(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}