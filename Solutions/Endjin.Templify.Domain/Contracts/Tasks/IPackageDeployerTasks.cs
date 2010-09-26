namespace Endjin.Templify.Domain.Contracts.Tasks
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    #endregion

    public interface IPackageDeployerTasks
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        void DeployPackage(CommandOptions commandOptions);

        IEnumerable<Package> RetrieveAllPackages();
    }
}