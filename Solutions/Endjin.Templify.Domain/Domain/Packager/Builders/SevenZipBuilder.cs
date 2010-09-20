namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    using ICSharpCode.SharpZipLib.Zip;

    using SevenZip;

    #endregion

    [Export(typeof(IArchiveBuilder))]
    public class SevenZipBuilder : IArchiveBuilder
    {
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public SevenZipBuilder(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Build(Package package, string path)
        {
            var archiveName = package.Manifest.Name.ToLowerInvariant().Replace(" ", "-") + "-v" + package.Manifest.Version;
            var archive = Path.Combine(FilePaths.PackageRepository, archiveName) + FileTypes.Package;

            var file = new FileInfo(Assembly.GetExecutingAssembly().Location);

            SevenZipCompressor.SetLibraryPath(Path.Combine(file.DirectoryName, "7z.dll"));
            
            var sevenZipCompressor = new SevenZipCompressor
                {
                    ArchiveFormat = OutArchiveFormat.SevenZip, 
                    CompressionLevel = CompressionLevel.Ultra
                };

            sevenZipCompressor.Compressing += this.Compressing;
            sevenZipCompressor.CompressionFinished += this.CompressingFinished;

            sevenZipCompressor.CompressFiles(archive, package.Manifest.Files.Select(manifestFile => manifestFile.File).ToArray());
        }

        private void CompressingFinished(object sender, EventArgs e)
        {
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 0, 0);
        }

        private void Compressing(object sender, ProgressEventArgs e)
        {
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 100, e.PercentDone);
        }
    }
}