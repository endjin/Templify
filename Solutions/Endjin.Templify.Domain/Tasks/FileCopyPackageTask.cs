namespace Endjin.Templify.Domain.Tasks
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageTask))]
    public class FileCopyPackageTask : IPackageTask
    {
        #region Fields

        private readonly IArchiveProcessor archiveProcessor;
        private readonly IEnvironmentalTokenResolver environmentalTokenResolver;
        private readonly IProgressNotifier progressNotifier;
        private readonly IReservedTokenResolver reservedTokenResolver;

        private Manifest manifest;

        #endregion
        
        [ImportingConstructor]
        public FileCopyPackageTask(
            IArchiveProcessor archiveProcessor,
            IEnvironmentalTokenResolver environmentalTokenResolver, 
            IProgressNotifier progressNotifier, 
            IReservedTokenResolver reservedTokenResolver)
        {
            this.archiveProcessor = archiveProcessor;
            this.environmentalTokenResolver = environmentalTokenResolver;
            this.progressNotifier = progressNotifier;
            this.reservedTokenResolver = reservedTokenResolver;
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
                
                // resolve any tokens in the base path information
                string baseDestPath = this.reservedTokenResolver.Resolve(dest, this.manifest.InstallRoot);
                baseDestPath = this.environmentalTokenResolver.Resolve(baseDestPath);

                // ensure that files get installed into the correct location if they have specific InstallPath
                // irrespective of any folder structure within the manifest file.
                string destFilePath = Path.Combine(baseDestPath, String.IsNullOrEmpty(manifestFile.InstallPath) ? manifestFile.File : Path.GetFileName(manifestFile.File));

                if (!Directory.Exists(Path.GetDirectoryName(destFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
                }

                this.archiveProcessor.Extract(this.manifest.Path, manifestFile.File, destFilePath);
                
                this.progressNotifier.UpdateProgress(ProgressStage.FileCopy, this.manifest.Files.Count, progress);
            }
        }
    }
}