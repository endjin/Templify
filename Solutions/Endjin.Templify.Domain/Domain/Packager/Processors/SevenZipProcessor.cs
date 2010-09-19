namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;

    using SevenZip;

    #endregion

    [Export(typeof(IArchiveProcessor))]
    public class SevenZipProcessor : IArchiveProcessor
    {
        public void Extract(string archivePath, string filePath, string destFilePath)
        {
            using (var destFileStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write))
            {
                this.SetLibraryPath();

                var extractor = new SevenZipExtractor(archivePath);
                extractor.ExtractionFinished += this.ExtractionFinished;

                extractor.ExtractFile(filePath, destFileStream);

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