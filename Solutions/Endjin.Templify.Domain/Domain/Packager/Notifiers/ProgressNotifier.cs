namespace Endjin.Templify.Domain.Domain.Packager.Notifiers
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;

    using Endjin.Templify.Domain.Contracts.Packager.Notifiers;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IProgressNotifier))]
    public class ProgressNotifier : IProgressNotifier
    {
        public event EventHandler<PackageProgressEventArgs> Progress;

        public void UpdateProgress(ProgressStage progressStage, int maxValue, int currentValue)
        {
              if (this.Progress != null)
              {
                  this.Progress(this, new PackageProgressEventArgs(progressStage, maxValue, currentValue));
              }
        }
    }
}