namespace Endjin.Templify.Domain.Domain.Packages
{
    using System.Xml.Serialization;

    public class PackageConfigurationData
    {
        [XmlAttribute]
        public string Token { get; set; }

        [XmlText]
        public string Description { get; set; }
        
        [XmlAttribute]
        public PackageConfigurationDataKind Kind { get; set; }

        [XmlIgnore]
        public string Value { get; set; }
    }

    public enum PackageConfigurationDataKind
    {
        text = 0,
        flag = 1,
        choice = 2,
        password = 3
    }
}
