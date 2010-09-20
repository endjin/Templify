namespace Endjin.Templify.Domain.Contracts.Packager.Filters
{
    #region Using Directives

    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IBinaryFileFilter
    {
        IEnumerable<ManifestFile> Filter(IEnumerable<ManifestFile> files);
        
        IEnumerable<string> Filter(IEnumerable<string> files);
    }
}