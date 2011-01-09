namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Domain.Contracts.Framework.Loggers;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;

    using Hardcodet.Wpf.TaskbarNotification;

    #endregion

    [Export(typeof(ICreatePackageView))]
    public partial class CreatePackageViewModel : PropertyChangedBase, ICreatePackageView
    {
        [ImportingConstructor]
        public CreatePackageViewModel(
            IErrorLogger errorLogger,
            INotificationManager notificationManager,
            IPackageCreatorTasks packageCreatorTasks,
            IWindowManager windowManager,
            IManageExclusionsView manageExclusionsView)
        {
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
            this.packageCreatorTasks = packageCreatorTasks;
            this.windowManager = windowManager;
            this.manageExclusionsView = manageExclusionsView;
            this.packageCreatorTasks.Progress += this.OnProgressUpdate;
        }

        public void CreatePackage()
        {
            this.CreatingPackage = true;

            try
            {
                BackgroundWorkerManager.RunBackgroundWork(this.ExecuteCreatePackage, this.ExecuteCreatePackageComplete);
            }
            catch (Exception exception)
            {
                this.errorLogger.Log(exception);
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
            // HACK: Until we get the dynamic UI Sorted
            this.CommandOptions.Tokens.Add(this.Token,"__NAME__");

            this.packageCreatorTasks.CreatePackage(this.CommandOptions);
        }

        private void ExecuteCreatePackageComplete(RunWorkerCompletedEventArgs e)
        {
            this.CreatingPackage = false;

            if (e.Error == null)
            {
                this.notificationManager.ShowNotification("Templify", "Package Created and Deployed to the Package Repository.");
            }
            else
            {
                this.errorLogger.Log(e.Error);
                this.notificationManager.ShowNotification("Templify", e.Error.Message);
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