namespace Endjin.Templify.Domain.Contracts.Packager.Notifiers
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IProgressNotifier
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        void UpdateProgress(ProgressStage progressStage, int maxValue, int currentValue);
    }
}