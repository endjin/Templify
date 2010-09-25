namespace Endjin.Templify.Domain.Contracts.Tasks
{
    using Endjin.Templify.Domain.Infrastructure;

    public interface IPackageCreatorTasks
    {
        void CreatePackage(CommandOptions commandOptions);
    }
}