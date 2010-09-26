namespace Endjin.Templify.Domain.Contracts.Tasks
{
    using System;

    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Infrastructure;

    public interface IPackageCreatorTasks
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        void CreatePackage(CommandOptions commandOptions);
    }
}