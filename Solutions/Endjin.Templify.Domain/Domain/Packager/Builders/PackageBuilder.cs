namespace Endjin.Templify.Domain.Domain.Packager.Builders
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;

    using Endjin.Templify.Domain.Contracts.Packager.Builders;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageBuilder))]
    public class PackageBuilder : IPackageBuilder
    {
        private readonly IManifestBuilder manifestBuilder;

        [ImportingConstructor]
        public PackageBuilder(IManifestBuilder manifestBuilder)
        {
            this.manifestBuilder = manifestBuilder;
            this.manifestBuilder.Progress += this.OnProgressChanged;
        }

        public event EventHandler<PackageProgressEventArgs> Progress;

        public Package Build(string path, IPackageMetaData packageMetaData)
        {
            return new Package { Manifest = this.manifestBuilder.Build(path, packageMetaData) };
        }

        private void OnProgressChanged(object sender, PackageProgressEventArgs e)
        {
            if (this.Progress != null)
            {
                this.Progress(sender, e);
            }
        }
    }
}