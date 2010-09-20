namespace Endjin.Templify.Domain.Domain.Packager.Processors
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(ICleanUpProcessor))]
    public class CleanUpProcessor : ICleanUpProcessor
    {
        private readonly IProgressNotifier progressNotifier;

        [ImportingConstructor]
        public CleanUpProcessor(IProgressNotifier progressNotifier)
        {
            this.progressNotifier = progressNotifier;
        }

        public void Process(string path)
        {
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 2, 1);
            
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            
            this.progressNotifier.UpdateProgress(ProgressStage.CreatingArchive, 2, 2);
        }
    }
}