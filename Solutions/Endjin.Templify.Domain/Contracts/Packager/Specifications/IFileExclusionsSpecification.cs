using System.Collections.Generic;
using Endjin.Templify.Domain.Contracts.Framework.Specifications;

namespace Endjin.Templify.Domain.Contracts.Packager.Specifications
{
    public interface IFileExclusionsSpecification : ILinqSpecification<string>
    {
        List<string> FileExclusions { get; set; }
        List<string> DirectoryExclusions { get; set; }
    }
}