namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;
    using System.Threading.Tasks;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Domain.Factories;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IClonePackageBuilder))]
    public class ClonePackageBuilder : IClonePackageBuilder
    {
        private readonly ICloneFileProcessor cloneFileProcessor;

        [ImportingConstructor]
        public ClonePackageBuilder(ICloneFileProcessor cloneFileProcessor)
        {
            this.cloneFileProcessor = cloneFileProcessor;
        }

        public Package Build(Package package)
        {
            package.ClonedPath = Path.Combine(Path.Combine(FilePaths.TemporaryPackageRepository, package.Manifest.Id.ToString()), "Cloned");
            package.TemplatePath = Path.Combine(Path.Combine(FilePaths.TemporaryPackageRepository, package.Manifest.Id.ToString()), "Template");

            var manifestFilePath = this.PersistManifestFileAndReturnLocation(package);

            Parallel.ForEach(
                package.Manifest.Files,
                file =>
                    {
                        var clonedPath = Path.Combine(package.ClonedPath, file.File);
                        
                        this.cloneFileProcessor.Process(Path.Combine(package.Manifest.Path, file.File), clonedPath);
                        
                        file.File = clonedPath;
                    });
            
            // Add the manifest file so that it will be tokenised.
            package.Manifest.Files.Add(new ManifestFile { File = manifestFilePath });

            return package;
        }

        private string PersistManifestFileAndReturnLocation(Package package)
        {
            var manifestFilePath = Path.Combine(package.ClonedPath, "manifest.xml");

            ManifestFactory.Save(manifestFilePath, package.Manifest);

            return manifestFilePath;
        }
    }
}