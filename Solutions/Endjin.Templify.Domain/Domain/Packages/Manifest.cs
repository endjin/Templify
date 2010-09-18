namespace Endjin.Templify.Domain.Domain.Packages
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Endjin.Templify.Domain.Contracts.Packages;

    #endregion

    [XmlRoot("Manifest")]
    public class Manifest : IPackageMetaData
    {
        public Manifest()
        {
            this.Files = new List<ManifestFile>();
        }

        public string Author { get; set; }

        [XmlArray]
        [XmlArrayItem("File")]
        public List<ManifestFile> Files { get; set; }

        public Guid Id { get; set; }

        public string InstallRoot { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public string Path { get; set; }

        public string Version { get; set; }

        [XmlIgnore]
        public string Title
        {
            get { return this.Name + " - " + this.Version; }
        }
    }
}