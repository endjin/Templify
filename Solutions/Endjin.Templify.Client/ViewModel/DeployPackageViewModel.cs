namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework.Threading;

    #endregion

    [Export(typeof(IDeployPackageView))]
    public class DeployPackageViewModel : PropertyChangedBase, IDeployPackageView
    {
        #region Fields

        private readonly IPackageTask packageTask;
        private readonly IPackageProcessor packageProcessor;
        private readonly IPackageRepository packageRepository;

        private PackageCollection packages;
        private string name;

        private int maxProgress = 0;

        private int currentProgress = 0;

        private Package selectedPackage;

        #endregion

        [ImportingConstructor]
        public DeployPackageViewModel(IPackageTask packageTask, IPackageProcessor packageProcessor, IPackageRepository packageRepository)
        {
            this.packageTask = packageTask;
            this.packageProcessor = packageProcessor;
            this.packageRepository = packageRepository;
            this.packageProcessor.Progress += this.OnPackageTaskProgress;
            this.packageTask.Progress += this.OnPackageTaskProgress;
        }

        #region Properties

        public bool CanDeployPackage
        {
            get { return !string.IsNullOrWhiteSpace(this.Name) && this.SelectedPackage != null; }
        }

        public int MaxProgress
        {
            get
            {
                return this.maxProgress;
            }

            set
            {
                this.maxProgress = value;
                this.NotifyOfPropertyChange(() => this.MaxProgress);
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
                this.name = value;
                this.NotifyOfPropertyChange(() => this.Name);
                this.NotifyOfPropertyChange(() => this.CanDeployPackage);
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
                this.selectedPackage = value;
                this.NotifyOfPropertyChange(() => this.SelectedPackage);
                this.NotifyOfPropertyChange(() => this.CanDeployPackage);
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
                this.packages = value;
                this.NotifyOfPropertyChange(() => this.Packages);
                this.NotifyOfPropertyChange(() => this.CanDeployPackage);
            }
        }

        public string Path
        {
            get;
            set;
        }

        #endregion

        public void DeployPackage()
        {
            var package = this.SelectedPackage;

            package.Manifest.InstallRoot = this.Path;

            this.ExecutePackage(package);
        }

        private void ExecutePackage(Package package)
        {
            BackgroundWorkerManager.RunBackgroundWork(() => this.ExecutePackageCore(package), this.ExecutePackageComplete);
        }

        private void ExecutePackageComplete(RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Package Sucessfully Deployed");
            }
            else
            {
                // Do some error handling
            }
        }

        private void ExecutePackageCore(Package package)
        {
            this.packageTask.Execute(package);
            this.packageProcessor.Process(this.Path, this.Name);
        }
        
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        private void Initialise()
        {
            BackgroundWorkerManager.RunBackgroundWork(this.RetrievePackages);
        }

        private void OnPackageTaskProgress(object sender, PackageProgressEventArgs e)
        {
            this.CurrentProgress = e.CurrentValue;
            this.MaxProgress = e.MaxValue;
        }

        private void RetrievePackages()
        {
            this.Packages = new PackageCollection(this.packageRepository.FindAll());
        }
    }
}