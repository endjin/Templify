namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IManifestBuilder))]
    public class ManifestBuilder : IManifestBuilder
    {
        private readonly IArtefactProcessor artefactProcessor;

        [ImportingConstructor]
        public ManifestBuilder([Import("FilteredFileSystemArtefactProcessor")]IArtefactProcessor artefactProcessor)
        {
            this.artefactProcessor = artefactProcessor;
        }

        public event EventHandler<PackageProgressEventArgs> Progress;

        public Manifest Build(string path, IPackageMetaData packageMetaData)
        {
            var files = this.artefactProcessor.RetrieveFiles(path);

            var manifest = new Manifest
                {
                    Author = packageMetaData.Author,
                    Id = Guid.NewGuid(),
                    Name = packageMetaData.Name,
                    Version = packageMetaData.Version,
                    Path = path
                };

            int progress = 0;

            foreach (var file in files)
            {
                manifest.Files.Add(new ManifestFile { File = StripParentPath(path, file) });
                this.OnProgressChanged(new PackageProgressEventArgs(files.Count(), progress));
                progress++;
            }

            return manifest;
        }

        protected virtual void OnProgressChanged(PackageProgressEventArgs e)
        {
            if (this.Progress != null)
            {
                this.Progress(this, e);
            }
        }

        private static string StripParentPath(string parentPath, string filePath)
        {
            return filePath.Replace(string.Concat(parentPath, "\\"), string.Empty);
        }
    }
}