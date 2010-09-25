namespace Endjin.Templify.Domain.Contracts.Tasks
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageDeploymentProcessor
    {
        void Execute(Package package);
    }
}
