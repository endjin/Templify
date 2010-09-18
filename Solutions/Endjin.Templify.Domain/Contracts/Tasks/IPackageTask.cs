namespace Endjin.Templify.Domain.Contracts.Tasks
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageTask
    {
        void Execute(Package package);
    }
}
