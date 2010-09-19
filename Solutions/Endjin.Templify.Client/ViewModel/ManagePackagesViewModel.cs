using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Endjin.Templify.Client.Contracts;
using Endjin.Templify.Client.Domain;
using Endjin.Templify.Domain.Contracts.Packages;
using Endjin.Templify.Domain.Domain.Packages;
using Endjin.Templify.Domain.Framework.Threading;

namespace Endjin.Templify.Client.ViewModel
{
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

        public string Path
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
            this.packageRepository.Remove(package);
            this.Packages.Remove(package);
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