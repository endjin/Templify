namespace Endjin.Templify.Domain.Tasks
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IPackageCreatorTasks))]
    public class PackageCreatorTasks : IPackageCreatorTasks
    {
        #region Fields

        private readonly IArchiveBuilder archiveBuilder;
        private readonly ICleanUpProcessor cleanUpProcessor;
        private readonly IClonePackageBuilder clonePackageBuilder;
        private readonly IPackageBuilder packageBuilder;
        private readonly IPackageTokeniser packageTokeniser;
        private readonly IProgressNotifier progressNotifier;

        private CommandOptions commandOptions;

        #endregion

        [ImportingConstructor]
        public PackageCreatorTasks(
            IArchiveBuilder archiveBuilder,
            ICleanUpProcessor cleanUpProcessor,
            IClonePackageBuilder clonePackageBuilder,
            IPackageBuilder packageBuilder,
            IPackageTokeniser packageTokeniser,
            IProgressNotifier progressNotifier)
        {
            this.archiveBuilder = archiveBuilder;
            this.cleanUpProcessor = cleanUpProcessor;
            this.clonePackageBuilder = clonePackageBuilder;
            this.packageBuilder = packageBuilder;
            this.packageTokeniser = packageTokeniser;
            this.progressNotifier = progressNotifier;
            this.progressNotifier.Progress += this.OnProgressUpdate;
        }
        
        public event EventHandler<PackageProgressEventArgs> Progress;

        #region Properties

        public int CurrentProgress { get; set; }

        public int MaxProgress { get; set; }

        public string ProgressStatus { get; set; }

        #endregion

        public void CreatePackage(CommandOptions options)
        {
            this.commandOptions = options;
            BackgroundWorkerManager.RunBackgroundWork(this.RunCreatePackage, this.RunCreatePackageComplete);
        }

        private void RunCreatePackage()
        {
            var packageMetaData = new PackageMetaData
                {
                    Author = this.commandOptions.Author,
                    Name = this.commandOptions.Name,
                    Tokens = this.commandOptions.Tokens.Select(kvp => kvp.Value).ToList(),
                    Version = this.commandOptions.Version
                };

            var package = this.packageBuilder.Build(this.commandOptions.Path, packageMetaData);

            var clonedPackage = this.clonePackageBuilder.Build(package);
            var tokenisedPackage = this.packageTokeniser.Tokenise(clonedPackage, this.commandOptions.Tokens);

            this.archiveBuilder.Build(tokenisedPackage, this.commandOptions.Path);
            this.cleanUpProcessor.Process(FilePaths.TemporaryPackageRepository);
        }

       private void RunCreatePackageComplete(RunWorkerCompletedEventArgs e)
       {
            if (e.Error == null)
            {
            }
        }

        private void OnProgressUpdate(object sender, PackageProgressEventArgs e)
        {
            if (this.Progress != null)
            {
                this.Progress(sender, e);
            }

            //this.CurrentProgress = e.CurrentValue;
            //this.MaxProgress = e.MaxValue;
            //this.ProgressStatus = e.ProgressStage.GetDescription();
        }
    }
}