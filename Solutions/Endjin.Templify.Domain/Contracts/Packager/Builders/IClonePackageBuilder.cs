namespace Endjin.Templify.Domain.Contracts.Packager.Builders
{
    using Endjin.Templify.Domain.Domain.Packages;

    public interface IClonePackageBuilder
    {
        Package Build(Package package);
    }
}