namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    using ICSharpCode.SharpZipLib.Zip;

    #endregion

    //[Export(typeof(IArchiveBuilder))]
    public class ZipBuilder : IArchiveBuilder
    {
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public ZipBuilder(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Build(Package package, string path)
        {
            var archiveName = package.Manifest.Name.ToLowerInvariant().Replace(" ", "-") + "-v" + package.Manifest.Version;
            var archive = ZipFile.Create(Path.Combine(FilePaths.PackageRepository, archiveName) + FileTypes.Package);
            
            archive.BeginUpdate();

            int progress = 0;
            int fileCount = package.Manifest.Files.Count;

            foreach (var manifestFile in package.Manifest.Files)
            {
                archive.Add(Path.Combine(path, manifestFile.File), manifestFile.File.Replace(package.TemplatePath, string.Empty));
                this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, fileCount, progress);
                progress++;
            }

            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, ProgressStep.StepTwo, ProgressStep.StepOne);
            archive.CommitUpdate();
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, ProgressStep.StepTwo, ProgressStep.StepTwo);
            archive.Close();
        }
    }
}