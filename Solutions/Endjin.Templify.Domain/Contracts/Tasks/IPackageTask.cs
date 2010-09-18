namespace Endjin.Templify.Domain.Contracts.Tasks
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageTask
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        void Execute(Package package);
    }
}
