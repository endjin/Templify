using System.IO;
using ICSharpCode.SharpZipLib.Zip;

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
<<<<<<< HEAD
        public static Package Get(string path)
        {
            var package = new Package();
            ZipFile packageFile = null;
            ZipEntry manifestFile;
            Stream manifestXmlStream;

            try
            {
                packageFile = new ICSharpCode.SharpZipLib.Zip.ZipFile(path);
                manifestFile = packageFile.GetEntry("manifest.xml");
                manifestXmlStream = packageFile.GetInputStream(manifestFile.ZipFileIndex);
=======
        private readonly IArchiveProcessor archiveProcessor;

        [ImportingConstructor]
        public PackageFactory(IArchiveProcessor archiveProcessor)
        {
            this.archiveProcessor = archiveProcessor;
        }
>>>>>>> 586950baa18542c5f3c608982188c4b81789cd85

        public Package Get(string path)
        {
            Manifest manifest;

            using (var manifestXmlStream = this.archiveProcessor.Extract(path, "manifest.xml"))
            {
                var serializer = new XmlSerializer(typeof(Manifest));
                manifest = (Manifest)serializer.Deserialize(manifestXmlStream);
            }
            finally
            {
                if (packageFile != null)
                {
                    packageFile.Close();
                }
            }

            manifest.Path = path;

            var package = new Package { Manifest = manifest };

            return package;
        }
    }
}