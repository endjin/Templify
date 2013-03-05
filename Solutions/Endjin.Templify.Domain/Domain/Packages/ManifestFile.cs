namespace Endjin.Templify.Domain.Domain.Packages
{
    using System.Xml.Serialization;

    public class ManifestFile
    {
        [XmlText]
        public string File { get; set; }

        [XmlAttribute]
        public string InstallPath { get; set; }

        [XmlAttribute]
        public string TempPath { get; set; }
    }
}