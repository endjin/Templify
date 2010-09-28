namespace Endjin.Templify.Domain.Contracts.Packager.Builders
{
    using Endjin.Templify.Domain.Domain.Packages;

    public interface IArchiveBuilder
    {
        void Build(Package package, string path, string packageRepositoryPath);
    }
}