namespace Endjin.Templify.CommandLine
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Container;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public class Client
    {
        public Client()
        {
            MefContainer.Compose(this);

            this.PackageCreatorTasks.Progress += this.OnProgressChanged;
            this.PackageDeployerTasks.Progress += this.OnProgressChanged;
        }

        [Import]
        private ICommandLineProcessor CommandLineProcessor { get; set; }

        [Import]
        private IPackageCreatorTasks PackageCreatorTasks { get; set; }

        [Import]
        private IPackageDeployerTasks PackageDeployerTasks { get; set; } 

        public void Execute(string[] args)
        {
            var options = this.CommandLineProcessor.Process(args);

            switch (options.Mode)
            {
                case Mode.Create:
                    this.PackageCreatorTasks.CreatePackage(options);
                    break;
                case Mode.Deploy:
                    this.PackageDeployerTasks.DeployPackage(options);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnProgressChanged(object sender, Domain.Domain.Packages.PackageProgressEventArgs e)
        {
            ConsoleProgress.Reset();
            ConsoleProgress.Update(e.CurrentValue, e.MaxValue, e.ProgressStage.GetDescription());
        }
    }
}