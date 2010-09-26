namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    using Endjin.Templify.Domain.Domain.Packages;

    public interface IPackageDeploymentProcessor
    {
        void Execute(Package package);
    }
}
