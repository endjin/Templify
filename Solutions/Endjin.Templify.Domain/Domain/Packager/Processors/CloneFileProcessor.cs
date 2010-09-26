namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    #endregion

    [Export(typeof(ICloneFileProcessor))]
    public class CloneFileProcessor : ICloneFileProcessor
    {
        public void Process(string sourcePath, string destinationPath)
        {
            var file = new FileInfo(destinationPath);

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            File.Copy(sourcePath, destinationPath);
        }
    }
}