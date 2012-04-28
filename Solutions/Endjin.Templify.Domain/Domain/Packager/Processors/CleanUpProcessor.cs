namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(ICleanUpProcessor))]
    public class CleanUpProcessor : ICleanUpProcessor
    {
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public CleanUpProcessor(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Process(string path)
        {
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 2, 1);
            
            if (Directory.Exists(path))
            {
                ForceDeleteDirectory(path);
            }
            
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 2, 2);
        }

        private static void ForceDeleteDirectory(string path)
        {
            var folders = new Stack<DirectoryInfo>();
            var root = new DirectoryInfo(path);
            folders.Push(root);

            while (folders.Count > 0)
            {
                DirectoryInfo currentFolder = folders.Pop();
                currentFolder.Attributes = currentFolder.Attributes & ~(FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.Hidden);

                foreach (var d in currentFolder.GetDirectories())
                {
                    folders.Push(d);
                }

                foreach (var fileInFolder in currentFolder.GetFiles())
                {
                    fileInFolder.Attributes = fileInFolder.Attributes & ~(FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.Hidden);
                    fileInFolder.Delete();
                }
            }

            root.Delete(true);
        }
    }
}