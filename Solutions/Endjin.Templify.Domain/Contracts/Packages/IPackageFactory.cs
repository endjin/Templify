namespace Endjin.Templify.Domain.Contracts.Packages
{
    using Endjin.Templify.Domain.Domain.Packages;

    public interface IPackageFactory
    {
        Package Get(string path);
    }
}