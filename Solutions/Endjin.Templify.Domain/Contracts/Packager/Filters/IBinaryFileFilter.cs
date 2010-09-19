namespace Endjin.Templify.Domain.Contracts.Packager.Filters
{
    #region Using Directives

    using System.Collections.Generic;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IBinaryFileFilter
    {
        List<ManifestFile> Filter(List<ManifestFile> files);
    }
}