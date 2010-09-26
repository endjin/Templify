namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IDeployPackageView))]
    public class DeployPackageViewModel : PropertyChangedBase, IDeployPackageView
    {
        #region Fields

        private readonly IPackageDeploymentProcessor packageDeploymentProcessor;
        private readonly IPackageProcessor packageProcessor;
        private readonly IPackageRepository packageRepository;
        private readonly IProgressNotifier progressNotifier;
        private readonly IWindowManager windowManager;
        private readonly IManagePackagesView managePackagesView;

        private int currentProgress;
        private bool deployingPackage;
        private int maxProgress;
        private string name;
        private PackageCollection packages;
        private string progressStatus;
        private Package selectedPackage;

        #endregion

        [ImportingConstructor]
        public DeployPackageViewModel(
            IPackageDeploymentProcessor packageDeploymentProcessor, 
            IPackageProcessor packageProcessor, 
            IPackageRepository packageRepository, 
            IProgressNotifier progressNotifier,
            IWindowManager windowManager, 
            IManagePackagesView managePackagesView)
        {
            this.packageDeploymentProcessor = packageDeploymentProcessor;
            this.packageProcessor = packageProcessor;
            this.packageRepository = packageRepository;
            this.progressNotifier = progressNotifier;
            this.windowManager = windowManager;
            this.managePackagesView = managePackagesView;
            this.progressNotifier.Progress += this.OnProgressUpdate;
        }

        #region Properties

        public bool CanDeployPackage
        {
            get { return !string.IsNullOrWhiteSpace(this.Name) && this.SelectedPackage != null; }
        }

        public int CurrentProgress
        {
            get
            {
                return this.currentProgress;
            }

            set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    this.NotifyOfPropertyChange(() => this.CurrentProgress);                    
                }
            }
        }

        public bool DeployingPackage
        {
            get
            {
                return this.deployingPackage;
            }

            private set
            {
                if (this.deployingPackage != value)
                {
                    this.deployingPackage = value;
                    this.NotifyOfPropertyChange(() => this.DeployingPackage);
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
                    this.NotifyOfPropertyChange(() => this.CanDeployPackage);
                }
            }
        }

        public PackageCollection Packages
        {
            get
            {
                if (this.packages == null)
                {
                    this.Initialise();
                }

                return this.packages;
            }

            set
            {
                if (this.packages != value)
                {
                    this.packages = value;
                    this.NotifyOfPropertyChange(() => this.Packages);
                    this.NotifyOfPropertyChange(() => this.CanDeployPackage);
                }
            }
        }

        public CommandOptions CommandOptions
        {
            get;
            set;
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

        public Package SelectedPackage
        {
            get
            {
                return this.selectedPackage;
            } 
 
            set
            {
                if (this.selectedPackage != value)
                {
                    this.selectedPackage = value;
                    this.NotifyOfPropertyChange(() => this.SelectedPackage);
                    this.NotifyOfPropertyChange(() => this.CanDeployPackage);
                }
            }
        }

        #endregion

        public void DeployPackage()
        {
            var package = this.SelectedPackage;

            package.Manifest.InstallRoot = this.CommandOptions.Path;

            this.ExecuteDeployPackage(package);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Manage()
        {
            this.windowManager.ShowDialog(this.managePackagesView, null);
            this.Initialise();
        }

        private void ExecuteDeployPackage(Package package)
        {
            this.DeployingPackage = true;

            try
            {
                BackgroundWorkerManager.RunBackgroundWork(() => this.ExecutePackageCore(package), this.ExecutePackageComplete);
            }
            catch (Exception)
            {
                // If we failed to start, then we must reset the deploying package state
                this.DeployingPackage = false;
                throw;
            }
        }

        private void ExecutePackageComplete(RunWorkerCompletedEventArgs e)
        {
            this.DeployingPackage = false;

            if (e.Error == null)
            {
                MessageBox.Show("Package Sucessfully Deployed");
            }
        }

        private void ExecutePackageCore(Package package)
        {
            this.packageDeploymentProcessor.Execute(package);
            this.packageProcessor.Process(this.CommandOptions.Path, this.CommandOptions.Tokens);
        }

        private void Initialise()
        {
            BackgroundWorkerManager.RunBackgroundWork(this.RetrievePackages);
        }

        private void OnProgressUpdate(object sender, PackageProgressEventArgs e)
        {
            this.CurrentProgress = e.CurrentValue;
            this.MaxProgress = e.MaxValue;
            this.ProgressStatus = e.ProgressStage.GetDescription();
        }

        private void RetrievePackages()
        {
            this.Packages = new PackageCollection(this.packageRepository.FindAll());
        }
    }
}