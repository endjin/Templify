namespace Endjin.Templify.Domain.Tasks
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageTask))]
    public class FileCopyPackageTask : IPackageTask
    {
        private readonly IProgressNotifier progressNotifier;

        private Manifest manifest;

        [ImportingConstructor]
        public FileCopyPackageTask(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Execute(Package package)
        {
            this.manifest = package.Manifest;
            int progress = 0;

            foreach (var manifestFile in this.manifest.Files)
            {
                progress++;

                // Set destination to the Package default, unless the file has an override defined
                string dest = String.IsNullOrEmpty(manifestFile.InstallPath) ? this.manifest.InstallRoot : manifestFile.InstallPath;

                var stream = this.GetFile(this.manifest.Path, manifestFile.File);
                
                // resolve any tokens in the base path information
                string baseDestPath = this.ResolveTokens(dest);

                // ensure that files get installed into the correct location if they have specific InstallPath
                // irrespective of any folder structure within the manifest file.
                string destFilePath = String.Empty;

                destFilePath = Path.Combine(baseDestPath, String.IsNullOrEmpty(manifestFile.InstallPath) ? manifestFile.File : Path.GetFileName(manifestFile.File));

                // Ensure that the destination directory exists
                if (!Directory.Exists(Path.GetDirectoryName(destFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
                }

                Console.WriteLine("Installing '{0}' to '{1}'", manifestFile.File, Path.GetDirectoryName(destFilePath));

                var destFileStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);
                
                stream.CopyTo(destFileStream);

                destFileStream.Flush();
                destFileStream.Close();
                
                stream.Close();
                
                this.progressNotifier.UpdateProgress(ProgressStage.FileCopy, this.manifest.Files.Count, progress);
            }
        }

        private Stream GetFile(string manifestPath, string file)
        {
            var arch = new ICSharpCode.SharpZipLib.Zip.ZipFile(manifestPath);
            var fileEntry = arch.GetEntry(file.Replace('\\', '/'));

            if (fileEntry == null)
            {
                throw new Exception(string.Format("File '{0}' not found in manifest '{1}'", file, manifestPath));
            }

            return arch.GetInputStream(fileEntry.ZipFileIndex);
        }

        private string ResolveTokens(string str)
        {
            string resolvedString = str;

            // Resolve any reserved tokens used by WPM
            resolvedString = resolvedString.Replace("$(InstallRoot)", this.manifest.InstallRoot);

            // Assume any remaining tokens are Environment variables
            while (resolvedString.IndexOf("$(") != -1)
            {
                int start = resolvedString.IndexOf("$(");
                int end = resolvedString.IndexOf(")", start);

                string envVar = resolvedString.Substring(start + 2, end - (start + 2));
                string envVarValue = Environment.GetEnvironmentVariable(envVar);

                if (!String.IsNullOrEmpty(envVarValue))
                {
                    resolvedString = resolvedString.Replace(string.Format("$({0})", envVar), envVarValue); 
                }                
            }

            return resolvedString;
        }
    }
}