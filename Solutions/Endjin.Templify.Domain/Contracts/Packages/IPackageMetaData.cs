namespace Endjin.Templify.Domain.Contracts.Packages
{
    using System.Collections.Generic;

    public interface IPackageMetaData
    {
        string Author { get; set; }

        string Name { get; set; }

        List<string> Tokens { get; set; }

        string Version { get; set; }
    }
}