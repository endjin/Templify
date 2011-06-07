using System.Collections.Generic;

namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

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
            DirectoryInfo currentFolder;
            Stack<DirectoryInfo> folders = new Stack<DirectoryInfo>();
            DirectoryInfo root = new DirectoryInfo(path);
            folders.Push(root);
            while (folders.Count > 0)
            {
                currentFolder = folders.Pop();
                currentFolder.Attributes = currentFolder.Attributes & ~(FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.Hidden);
                foreach (DirectoryInfo d in currentFolder.GetDirectories())
                {
                    folders.Push(d);
                }
                foreach (FileInfo fileInFolder in currentFolder.GetFiles())
                {
                    fileInFolder.Attributes = fileInFolder.Attributes & ~(FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.Hidden);
                    fileInFolder.Delete();
                }
            }

            root.Delete(true);
        }
    }
}