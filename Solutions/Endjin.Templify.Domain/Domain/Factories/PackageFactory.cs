namespace Endjin.Templify.Domain.Domain.Factories
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.IO;
    using System.Xml.Serialization;

    using Endjin.Templify.Domain.Contracts.Packager.Processors;
    using Endjin.Templify.Domain.Contracts.Packages;
    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    [Export(typeof(IPackageFactory))]
    public class PackageFactory : IPackageFactory
    {
        private readonly IArchiveProcessor archiveProcessor;

        [ImportingConstructor]
        public PackageFactory(IArchiveProcessor archiveProcessor)
        {
            this.archiveProcessor = archiveProcessor;
        }

        public Package Get(string path)
        {
            Manifest manifest;

            using (var manifestXmlStream = this.archiveProcessor.Extract(path, "manifest.xml"))
            {
                var serializer = new XmlSerializer(typeof(Manifest));
                manifest = (Manifest)serializer.Deserialize(manifestXmlStream);
            }

            manifest.Path = path;

            var package = new Package { Manifest = manifest };

            return package;
        }
    }
}