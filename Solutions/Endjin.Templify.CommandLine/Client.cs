namespace Endjin.Templify.CommandLine
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;

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
                case Mode.ShowTokens:
                    this.DisplayTokens(options);
                    break;
                case Mode.ListPackages:
                    this.DisplayAvailablePackages(options);
                    break;
                default:
                    Console.WriteLine(options.GetUsage());
                    break;
            }
        }

        private void DisplayTokens(CommandOptions options)
        {
            var tokens = this.PackageDeployerTasks.RetrieveTokensForPackage(options.PackageName);

            Console.WriteLine(string.Format("Tokens available in Package {0}:", options.PackageName));

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        private void DisplayAvailablePackages(CommandOptions options)
        {
            var packages = this.PackageDeployerTasks.RetrieveAllPackages();

            Console.WriteLine(string.Format("Templify packages available in repository '{0}':",
                                                options.PackageRepositoryPath));

            foreach (var package in packages)
            {
                Console.WriteLine("   {0}", package.Manifest.Name);
            }
        }

        private void OnProgressChanged(object sender, Domain.Domain.Packages.PackageProgressEventArgs e)
        {
            // TODO: Console Progress doesn't work inside msbuild
            // ConsoleProgress.Reset();
            // ConsoleProgress.Update(e.CurrentValue, e.MaxValue, e.ProgressStage.GetDescription());
        }
    }
}