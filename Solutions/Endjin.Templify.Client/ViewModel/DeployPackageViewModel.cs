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
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;

    #endregion

    [Export(typeof(IDeployPackageView))]
    public partial class DeployPackageViewModel : PropertyChangedBase, IDeployPackageView
    {
        [ImportingConstructor]
        public DeployPackageViewModel(
            INotificationManager notificationManager,
            IPackageDeployerTasks packageDeployerTasks,
            IWindowManager windowManager, 
            IManagePackagesView managePackagesView)
        {
            this.notificationManager = notificationManager;
            this.packageDeployerTasks = packageDeployerTasks;
            this.windowManager = windowManager;
            this.managePackagesView = managePackagesView;
            this.packageDeployerTasks.Progress += this.OnProgressUpdate;
        }

        public void DeployPackage()
        {
            this.DeployingPackage = true;

            try
            {
                BackgroundWorkerManager.RunBackgroundWork(this.ExecutePackage, this.ExecutePackageComplete);
            }
            catch (Exception)
            {
                // If we failed to start, then we must reset the deploying package state
                this.DeployingPackage = false;
                throw;
            }
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

        private void ExecutePackageComplete(RunWorkerCompletedEventArgs e)
        {
            this.DeployingPackage = false;

            if (e.Error == null)
            {
                this.notificationManager.ShowNotification("Templify", "Package Sucessfully Deployed");
            }
        }

        private void ExecutePackage()
        {
            this.CommandOptions.PackageName = this.SelectedPackage.Manifest.PackageName;

            // HACK: Until we get the dynamic UI Sorted
            this.CommandOptions.Tokens.Add("__NAME__", this.Name);

            this.packageDeployerTasks.DeployPackage(this.CommandOptions);
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
            this.Packages = new PackageCollection(this.packageDeployerTasks.RetrieveAllPackages());
        }
    }
}