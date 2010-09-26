namespace Endjin.Templify.Client.ViewModel
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Windows;

    using Caliburn.Micro;

    using Endjin.Templify.Client.Contracts;
    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packager.Tokeniser;
    using Endjin.Templify.Domain.Contracts.Tasks;
    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework;
    using Endjin.Templify.Domain.Framework.Threading;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public partial class CreatePackageViewModel
    {
        #region Fields

        private readonly IPackageCreatorTasks packageCreatorTasks;
        private readonly IWindowManager windowManager;
        private readonly IManageExclusionsView manageExclusionsView;

        private string author;
        private bool creatingPackage;
        private int currentProgress;
        private string name;
        private int maxProgress;
        private string progressStatus;
        private string token;
        private string version;

        #endregion

        #region Properties

        public string Author
        {
            get
            {
                return this.author;
            }

            set
            {
                if (this.author != value)
                {
                    this.author = value;
                    this.CommandOptions.Author = value;
                    this.NotifyOfPropertyChange(() => this.Author);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public bool CanCreatePackage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Author) &&
                       !string.IsNullOrWhiteSpace(this.Name) &&
                       !string.IsNullOrWhiteSpace(this.Token) &&
                       !string.IsNullOrWhiteSpace(this.Version);
            }
        }

        public bool CreatingPackage
        {
            get
            {
                return this.creatingPackage;
            }

            private set
            {
                if (this.creatingPackage != value)
                {
                    this.creatingPackage = value;
                    this.NotifyOfPropertyChange(() => this.CreatingPackage);
                }
            }
        }

        public int CurrentProgress
        {
            get
            {
                return this.currentProgress;
            }

            set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    this.NotifyOfPropertyChange(() => this.CurrentProgress);
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.CommandOptions.Name = value;
                    this.NotifyOfPropertyChange(() => this.Name);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public CommandOptions CommandOptions
        {
            get;
            set;
        }

        public string Version
        {
            get
            {
                return this.version;
            }

            set
            {
                if (this.version != value)
                {
                    this.version = value;
                    this.CommandOptions.Version = value;
                    this.NotifyOfPropertyChange(() => this.Version);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public int MaxProgress
        {
            get
            {
                return this.maxProgress;
            }

            set
            {
                if (this.maxProgress != value)
                {
                    this.maxProgress = value;
                    this.NotifyOfPropertyChange(() => this.MaxProgress);
                }
            }
        }

        public string Token
        {
            get
            {
                return this.token;
            }

            set
            {
                if (this.token != value)
                {
                    this.token = value;
                    this.NotifyOfPropertyChange(() => this.Token);
                    this.NotifyOfPropertyChange(() => this.CanCreatePackage);
                }
            }
        }

        public string ProgressStatus
        {
            get
            {
                return this.progressStatus;
            }

            set
            {
                if (this.progressStatus != value)
                {
                    this.progressStatus = value;
                    this.NotifyOfPropertyChange(() => this.ProgressStatus);
                }
            }
        }

        #endregion
    }
}