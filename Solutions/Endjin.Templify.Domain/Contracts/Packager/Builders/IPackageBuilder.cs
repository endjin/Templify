namespace Endjin.Templify.Domain.Contracts.Packager.Builders
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageBuilder
    {
        Package Build(string path, IPackageMetaData packageMetaData);
    }
}