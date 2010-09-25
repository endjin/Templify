namespace Endjin.Templify.Domain.Tasks
{
    #region Using Directives

    using System.ComponentModel;
    using System.ComponentModel.Composition;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IPackageDeployerTasks))]
    public class PackageDeployerTasks : IPackageDeployerTasks
    {
        #region Fields

        private readonly IProgressNotifier progressNotifier;
        private readonly IPackageDeploymentProcessor packageDeploymentProcessor;
        private readonly IPackageProcessor packageProcessor;
        private readonly IPackageRepository packageRepository;

        #endregion

        [ImportingConstructor]
        public PackageDeployerTasks(
         IPackageDeploymentProcessor packageDeploymentProcessor,
            IPackageProcessor packageProcessor,
            IPackageRepository packageRepository, 
            IProgressNotifier progressNotifier)
        {
            this.packageDeploymentProcessor = packageDeploymentProcessor;
            this.packageProcessor = packageProcessor;
            this.packageRepository = packageRepository;
            this.progressNotifier = progressNotifier;
            this.progressNotifier.Progress += this.OnProgressUpdate;
        }

        #region Properties

        public int CurrentProgress { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int MaxProgress { get; set; }

        public string ProgressStatus { get; set; }

        #endregion

        public void DeployPackage(CommandOptions commandOptions)
        {
            //BackgroundWorkerManager.RunBackgroundWork(() => this.RunDeployPackage(package), this.RunPackageComplete);
        }

        private void RunPackageComplete(RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
            }
        }

        private void RunDeployPackage(Package package)
        {
            this.packageDeploymentProcessor.Execute(package);
            this.packageProcessor.Process(this.Path, this.Name);
        }

        private void OnProgressUpdate(object sender, PackageProgressEventArgs e)
        {
            this.CurrentProgress = e.CurrentValue;
            this.MaxProgress = e.MaxValue;
            this.ProgressStatus = e.ProgressStage.GetDescription();
        }
    }
}