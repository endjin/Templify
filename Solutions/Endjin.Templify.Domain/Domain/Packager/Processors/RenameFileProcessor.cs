namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    #endregion

    [Export(typeof(IRenameFileProcessor))]
    public class RenameFileProcessor : IRenameFileProcessor
    {
        public void Process(string oldName, string newName)
        {
            var file = new FileInfo(newName);

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            File.Move(oldName, newName);
        }
    }
}