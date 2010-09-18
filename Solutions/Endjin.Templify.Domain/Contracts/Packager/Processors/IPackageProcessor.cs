namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    #region Using Directives

    using System;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IPackageProcessor
    {
        void Process(string path, string name);
    }
}