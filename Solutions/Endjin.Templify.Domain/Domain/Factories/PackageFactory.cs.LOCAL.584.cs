using System.IO;
using ICSharpCode.SharpZipLib.Zip;

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
            ZipFile packageFile = null;
            ZipEntry manifestFile;
            Stream manifestXmlStream;

            try
            {
                packageFile = new ICSharpCode.SharpZipLib.Zip.ZipFile(path);
                manifestFile = packageFile.GetEntry("manifest.xml");
                manifestXmlStream = packageFile.GetInputStream(manifestFile.ZipFileIndex);

                var serializer = new XmlSerializer(typeof(Manifest));
                var manifest = (Manifest)serializer.Deserialize(manifestXmlStream);

                manifest.Path = path;
                
                package = new Package { Manifest = manifest };
            }
            catch
            {
            }
            finally
            {
                if (packageFile != null)
                {
                    packageFile.Close();
                }
            }

            return package;
        }
    }
}