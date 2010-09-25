namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IManagePackagesView))]
    public class ManagePackagesViewModel : PropertyChangedBase, IManagePackagesView
    {
        private readonly IPackageRepository packageRepository;

        private PackageCollection packages;

        [ImportingConstructor]
        public ManagePackagesViewModel(IPackageRepository packageRepository)
        {
            this.packageRepository = packageRepository;
        }

        public CommandOptions CommandOptions
        {
            get;
            set;
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
                }
            }
        }

        public void Remove(Package package)
        {
            try
            {
                this.packageRepository.Remove(package);
                this.Packages.Remove(package);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Package Could not be deleted.");
            }

            this.NotifyOfPropertyChange(() => this.Packages);
        }

        private void Initialise()
        {
            BackgroundWorkerManager.RunBackgroundWork(this.RetrievePackages);
        }
        
        private void RetrievePackages()
        {
            this.Packages = new PackageCollection(this.packageRepository.FindAll());
        }
    }
}