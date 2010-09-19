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
            this.MakeWritable(path);

            File.WriteAllText(path, content);
        }

        private void MakeWritable(string path)
        {
            File.SetAttributes(path, File.GetAttributes(path) & ~(FileAttributes.Archive | FileAttributes.ReadOnly));
        }
    }
}