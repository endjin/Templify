namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Domain.Packages;

    using SevenZip;

    #endregion

    [Export(typeof(IArchiveProcessor))]
    public class SevenZipProcessor : IArchiveProcessor
    {
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public SevenZipProcessor(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Extract(string archivePath, List<ManifestFile> files)
        {
            this.SetLibraryPath();

            using (var extractor = new SevenZipExtractor(archivePath))
            {
                // extractor.ExtractionFinished += this.ExtractionFinished;

                int progress = 0;

                foreach (var manifestFile in files)
                {
                    progress++;

                    if (!Directory.Exists(Path.GetDirectoryName(manifestFile.InstallPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(manifestFile.InstallPath));
                    }

                    this.ExtractFile(extractor, manifestFile);

                    this.progressNotifier.UpdateProgress(ProgressStage.ExtractFilesFromPackage, files.Count, progress);
                }
            }
        }

        private void ExtractFile( SevenZipExtractor extractor, ManifestFile file)
        {
            using (var destFileStream = new FileStream(file.InstallPath, FileMode.Create, FileAccess.Write))
            { 
                try
                {
                    extractor.ExtractFile(file.File, destFileStream);
                }
                catch (Exception exception)
                {
                }

                destFileStream.Flush();
                destFileStream.Close();
            }
        }

        public Stream Extract(string archivePath, string filePath)
        {
            this.SetLibraryPath();

            var extractor = new SevenZipExtractor(archivePath);
            extractor.ExtractionFinished += this.ExtractionFinished;

            var stream = new MemoryStream();

            var matchingfile = extractor.ArchiveFileData.Where(x => x.FileName == filePath).FirstOrDefault();

            extractor.ExtractFile(matchingfile.Index, stream);

            // reset stream position to the start
            stream.Position = 0;

            return stream;
        }

        private void SetLibraryPath()
        {
            var file = new FileInfo(Assembly.GetExecutingAssembly().Location);

            SevenZipCompressor.SetLibraryPath(Path.Combine(file.DirectoryName, "7z.dll"));
        }

        private void ExtractionFinished(object sender, EventArgs e)
        {
            var extractor = sender as SevenZipExtractor;

            if (extractor != null)
            {
                extractor.Dispose();
            }
        }
    }
}