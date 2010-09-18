namespace Endjin.Templify.Domain.Contracts.Packager.Builders
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IManifestBuilder
    {
        event EventHandler<PackageProgressEventArgs> Progress;

        Manifest Build(string path, IPackageMetaData packageMetaData);
    }
}