namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Domain.Contracts.Infrastructure;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    [Export(typeof(IManageExclusionsView))]
    public class ManageExclusionsViewModel : PropertyChangedBase, IManageExclusionsView
    {
        private readonly IConfiguration configuration;
        private string fileExclusions;
        private string directoryExclusions;

        [ImportingConstructor]
        public ManageExclusionsViewModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CommandOptions CommandOptions
        {
            get;
            set;
        }

        public string FileExclusions
        {
            get
            {
                if (this.fileExclusions == null)
                {
                    this.Initialise();
                }

                return this.fileExclusions;
            }

            set
            {
                if (this.fileExclusions != value)
                {
                    this.fileExclusions = value;
                    this.NotifyOfPropertyChange(() => this.FileExclusions);
                }
            }
        }

        public string DirectoryExclusions
        {
            get
            {
                if (this.directoryExclusions == null)
                {
                    this.Initialise();
                }

                return this.directoryExclusions;
            }

            set
            {
                if (this.directoryExclusions != value)
                {
                    this.directoryExclusions = value;
                    this.NotifyOfPropertyChange(() => this.DirectoryExclusions);
                }
            }
        }

        public void Save()
        {
            this.configuration.SaveDirectoryExclusions(this.directoryExclusions);
            this.configuration.SaveFileExclusions(this.fileExclusions);

            MessageBox.Show("Settings have been saved.");
        }

        private void Initialise()
        {
            BackgroundWorkerManager.RunBackgroundWork(this.RetrieveConfiguration);
        }

        private void RetrieveConfiguration()
        {
            this.FileExclusions = this.configuration.GetFileExclusions();
            this.DirectoryExclusions = this.configuration.GetDirectoryExclusions();
        }
    }
}