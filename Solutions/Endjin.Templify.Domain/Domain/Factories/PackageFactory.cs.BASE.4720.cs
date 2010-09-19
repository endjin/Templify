namespace Endjin.Templify.Domain.Domain.Factories
{
    #region Using Directives

    using System.Xml.Serialization;

    using Endjin.Templify.Domain.Domain.Packages;

    #endregion

    public static class PackageFactory
    {
        public static Package Get(string path)
        {
            var package = new Package();

            try
            {
                var packageFile = new ICSharpCode.SharpZipLib.Zip.ZipFile(path);
                var manifestFile = packageFile.GetEntry("manifest.xml");

                var manifestXmlStream = packageFile.GetInputStream(manifestFile.ZipFileIndex);

                var serializer = new XmlSerializer(typeof(Manifest));
                var manifest = (Manifest)serializer.Deserialize(manifestXmlStream);

                manifest.Path = path;
                
                package = new Package { Manifest = manifest };
            }
            catch
            {
            }

            return package;
        }
    }
}