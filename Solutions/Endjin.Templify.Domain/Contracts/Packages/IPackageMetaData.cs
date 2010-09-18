namespace Endjin.Templify.Domain.Contracts.Packages
{
    public interface IPackageMetaData
    {
        string Author { get; set; }

        string Name { get; set; }

        string Version { get; set; }
    }
}