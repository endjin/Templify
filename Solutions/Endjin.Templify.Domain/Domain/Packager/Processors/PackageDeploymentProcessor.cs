namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageDeploymentProcessor))]
    public class PackageDeploymentProcessor : IPackageDeploymentProcessor
    {
        #region Fields

        private readonly IArchiveProcessor archiveProcessor;
        private readonly IEnvironmentalTokenResolver environmentalTokenResolver;
        private readonly IProgressNotifier progressNotifier;
        private readonly IReservedTokenResolver reservedTokenResolver;

        private Manifest manifest;

        #endregion
        
        [ImportingConstructor]
        public PackageDeploymentProcessor(
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

            foreach (var file in this.manifest.Files)
            {
                file.InstallPath = this.GetDestFilePath(file);
            }

            this.archiveProcessor.Extract(this.manifest.Path, package.Manifest.Files);
        }

        private string GetDestFilePath(ManifestFile manifestFile)
        {
            string dest = String.IsNullOrEmpty(manifestFile.InstallPath) ? this.manifest.InstallRoot : manifestFile.InstallPath;
                
            // resolve any tokens in the base path information
            string baseDestPath = this.reservedTokenResolver.Resolve(dest, this.manifest.InstallRoot);
            baseDestPath = this.environmentalTokenResolver.Resolve(baseDestPath);

            // ensure that files get installed into the correct location if they have specific InstallPath
            // irrespective of any folder structure within the manifest file.
            return Path.Combine(baseDestPath, String.IsNullOrEmpty(manifestFile.InstallPath) ? manifestFile.File : Path.GetFileName(manifestFile.File));
        }
    }
}