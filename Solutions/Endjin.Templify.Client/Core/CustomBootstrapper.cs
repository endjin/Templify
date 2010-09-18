namespace Endjin.Templify.Client.Core
{
    #region Using Directives

    using Caliburn.Micro;

    using CommandLine;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Client.Domain;

    #endregion

    public class CustomBootstrapper : MefBootstrapper<IShell>
    {
        #region Fields

        private Mode mode;

        private string path;

        #endregion

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
            var options = new Options();
            ICommandLineParser parser = new CommandLineParser();

            if (parser.ParseArguments(e.Args, options))
            {
                if (options.CreatePackagePath != null)
                {
                    this.mode = Mode.Create;
                    this.path = options.CreatePackagePath;
                }
                else if (options.DeployPackagePath != null)
                {
                    this.mode = Mode.Deploy;
                    this.path = options.DeployPackagePath;
                }
            }

            base.OnStartup(sender, e);
        }
    }
}