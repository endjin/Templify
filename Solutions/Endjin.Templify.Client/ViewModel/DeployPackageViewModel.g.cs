namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;
    using Endjin.Templify.Domain.Contracts.Framework.Loggers;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public partial class DeployPackageViewModel
    {
        #region Fields

        private readonly IErrorLogger errorLogger;
        private readonly INotificationManager notificationManager;
        private readonly IPackageDeployerTasks packageDeployerTasks;
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
    }
}