namespace Endjin.Templify.Client.Core
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public class CustomBootstrapper : MefBootstrapper<IShell>
    {
        #region Fields

        private string[] args;

        private Mode mode;

        #endregion

        [Import]
        public ICommandLineProcessor CommandLineProcessor { get; set; }

        protected override void DisplayRootView()
        {
            var options = this.CommandLineProcessor.Process(this.args);

            this.mode = options.Mode;

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

            rootModel.CommandOptions = options;

            manager.Show(rootModel, null);
        }

        protected override void Configure()
        {
            base.Configure(this);
            base.Configure();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            this.args = e.Args;

            base.OnStartup(sender, e);
        }
    }
}