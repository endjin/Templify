namespace Endjin.Templify.Domain.Contracts.Packager.Processors
{
    #region Using Directives

    using System.Collections.Generic;
    using System.IO;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public interface IArchiveProcessor
    {
        void Extract(string archivePath, List<ManifestFile> files);

        Stream Extract(string archivePath, string filePath);
    }
}