namespace Endjin.Templify.Domain.Contracts.Tasks
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public interface IPackageCreatorTasks
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        void CreatePackage(CommandOptions commandOptions);
    }
}