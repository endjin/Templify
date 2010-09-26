namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IManifestBuilder))]
    public class ManifestBuilder : IManifestBuilder
    {
        private readonly IArtefactProcessor artefactProcessor;
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public ManifestBuilder([Import("FilteredFileSystemArtefactProcessor")]IArtefactProcessor artefactProcessor, IProgressNotifier progressNotifier)
        {
            this.artefactProcessor = artefactProcessor;
            this.progressNotifier = progressNotifier;
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
                    Path = path,
                    Tokens = packageMetaData.Tokens,
                    Version = packageMetaData.Version,
                };

            int progress = 0;

            var fileCount = files.Count();
            var manifestFiles = new BlockingCollection<ManifestFile>();

            Parallel.ForEach(
                files,
                file =>
                    {
                        manifestFiles.Add(new ManifestFile { File = StripParentPath(path, file) });
                        this.progressNotifier.UpdateProgress(ProgressStage.BuildManifest, fileCount, progress);
                        progress++;
                    });

            manifest.Files.AddRange(manifestFiles);

            return manifest;
        }

        private static string StripParentPath(string parentPath, string filePath)
        {
            return filePath.Replace(string.Concat(parentPath, "\\"), string.Empty);
        }
    }
}