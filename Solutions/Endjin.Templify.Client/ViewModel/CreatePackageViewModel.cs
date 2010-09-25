namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(ICreatePackageView))]
    public class CreatePackageViewModel : PropertyChangedBase, ICreatePackageView
    {
        #region Fields

        private readonly IArchiveBuilder archiveBuilder;
        private readonly ICleanUpProcessor cleanUpProcessor;
        private readonly IClonePackageBuilder clonePackageBuilder;
        private readonly IPackageBuilder packageBuilder;
        private readonly IPackageTokeniser packageTokeniser;
        private readonly IProgressNotifier progressNotifier;
        private readonly IWindowManager windowManager;
        private readonly IManageExclusionsView manageExclusionsView;

        private string author;
        private bool creatingPackage;
        private int currentProgress;
        private string name;
        private int maxProgress;
        private string progressStatus;
        private string token;
        private string version;

        #endregion

        [ImportingConstructor]
        public CreatePackageViewModel(
            IArchiveBuilder archiveBuilder, 
            ICleanUpProcessor cleanUpProcessor, 
            IClonePackageBuilder clonePackageBuilder,
            IPackageBuilder packageBuilder,
            IPackageTokeniser packageTokeniser,
            IProgressNotifier progressNotifier,
            IWindowManager windowManager,
            IManageExclusionsView manageExclusionsView)
        {
            this.archiveBuilder = archiveBuilder;
            this.cleanUpProcessor = cleanUpProcessor;
            this.clonePackageBuilder = clonePackageBuilder;
            this.packageBuilder = packageBuilder;
            this.packageTokeniser = packageTokeniser;
            this.progressNotifier = progressNotifier;
            this.windowManager = windowManager;
            this.manageExclusionsView = manageExclusionsView;
            this.progressNotifier.Progress += this.OnProgressUpdate;
        }

        #region Properties

        public string Author
        {
            get
            {
                return this.author;
            }

            set
            {
                if (this.author != value)
                {
                    this.author = value;
                    this.NotifyOfPropertyChange(() => this.Author);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public bool CanCreatePackage
        {
            get 
            { 
                return !string.IsNullOrWhiteSpace(this.Author) && 
                       !string.IsNullOrWhiteSpace(this.Name) && 
                       !string.IsNullOrWhiteSpace(this.Token) && 
                       !string.IsNullOrWhiteSpace(this.Version); 
            }
        }

        public bool CreatingPackage
        {
            get
            {
                return this.creatingPackage;
            }

            private set
            {
                if (this.creatingPackage != value)
                {
                    this.creatingPackage = value;
                    this.NotifyOfPropertyChange(() => this.CreatingPackage);
                }
            }
        }

        public int CurrentProgress
        {
            get
            {
                return this.currentProgress;
            }

            set
            {
                this.currentProgress = value;
                this.NotifyOfPropertyChange(() => this.CurrentProgress);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyOfPropertyChange(() => this.Name);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public CommandOptions CommandOptions
        {
            get;
            set;
        }

        public string Version
        {
            get
            {
                return this.version;
            }

            set
            {
                if (this.version != value)
                {
                    this.version = value;
                    this.NotifyOfPropertyChange(() => this.Version);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public int MaxProgress
        {
            get
            {
                return this.maxProgress;
            }

            set
            {
                if (this.maxProgress != value)
                {
                    this.maxProgress = value;
                    this.NotifyOfPropertyChange(() => this.MaxProgress);
                }
            }
        }

        public string Token
        {
            get
            {
                return this.token;
            }

            set
            {
                if (this.token != value)
                {
                    this.token = value;
                    this.NotifyOfPropertyChange(() => this.Token);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public string ProgressStatus
        {
            get
            {
                return this.progressStatus;
            }

            set
            {
                this.progressStatus = value;
                this.NotifyOfPropertyChange(() => this.ProgressStatus);
            }
        }

        #endregion

        public void CreatePackage()
        {
            this.CreatingPackage = true;

            try
            {
                BackgroundWorkerManager.RunBackgroundWork(this.ExecuteCreatePackage, this.ExecuteCreatePackageComplete);
            }
            catch (Exception)
            {
                // If we threw on startup, then we are no longer creating the package
                this.CreatingPackage = false;
                throw;
            }
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void ManageExclusions()
        {
            this.windowManager.ShowDialog(this.manageExclusionsView, null);
        }

        private void ExecuteCreatePackage()
        {
            var package = this.packageBuilder.Build(this.CommandOptions.Path, new PackageMetaData { Author = this.Author, Name = this.Name, Version = this.Version });

            var clonedPackage = this.clonePackageBuilder.Build(package);
            var tokenisedPackage = this.packageTokeniser.Tokenise(clonedPackage, this.CommandOptions.Tokens);

            this.archiveBuilder.Build(tokenisedPackage, this.CommandOptions.Path);
            this.cleanUpProcessor.Process(FilePaths.TemporaryPackageRepository);
        }

        private void ExecuteCreatePackageComplete(RunWorkerCompletedEventArgs e)
        {
            this.CreatingPackage = false;

            if (e.Error == null)
            {
               MessageBox.Show("Package Created and Deployed to the Package Repository.");
            }
        }

        private void OnProgressUpdate(object sender, PackageProgressEventArgs e)
        {
            this.CurrentProgress = e.CurrentValue;
            this.MaxProgress = e.MaxValue;
            this.ProgressStatus = e.ProgressStage.GetDescription();
        }
    }
}