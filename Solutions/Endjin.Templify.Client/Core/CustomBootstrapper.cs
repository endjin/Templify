namespace Endjin.Templify.Client.Core
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;
    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public class CustomBootstrapper : MefBootstrapper<IShell>
    {
        #region Fields

        private Mode mode;

        private string path;

        #endregion

        [Import]
        public ICommandLineProcessor CommandLineProcessor { get; set; }

        protected override void DisplayRootView()
        {
            IPackageViewModel rootModel;

            var manager = IoC.Get<IWindowManager>();

            switch (this.mode)
            {
                case Mode.Create:
                    rootModel = IoC.Get<ICreatePackageView>();
                    break;
                case Mode.Deploy:
                    rootModel = IoC.Get<IDeployPackageView>();
                    break;
                default:
                    rootModel = IoC.Get<IShell>();
                    break;
            }

            rootModel.Path = this.path;

            manager.Show(rootModel, null);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            var options = this.CommandLineProcessor.Process(e.Args);

            this.mode = options.Mode;
            this.path = options.PackagePath;

            base.OnStartup(sender, e);
        }
    }
}