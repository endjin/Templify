namespace Endjin.Templify.Domain.Contracts.Tasks
{
    using Endjin.Templify.Domain.Infrastructure;

    public interface IPackageDeployerTasks
    {
        void DeployPackage(CommandOptions commandOptions);
    }
}