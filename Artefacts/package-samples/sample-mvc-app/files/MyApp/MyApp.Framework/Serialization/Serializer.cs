namespace MyApp.Framework.Serialization
{
    #region Using Directives

    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    #endregion

    public static class Serializer
    {
        private static readonly object SyncRoot = new object();

        public static T CreateInstance<T>(string serializedType) where T : new()
        {
            var customType = new T();

            var serializer = new XmlSerializer(typeof(T));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(serializedType);
            var xmlNodeReader = new XmlNodeReader(xmlDocument);

            try
            {
                customType = (T)serializer.Deserialize(xmlNodeReader);
            }
            catch
            {
                // TODO: Add Logging etc here...
                throw;
            }

            return customType;
        }

        public static void SaveInstance<T>(T t, string fileName) where T : new()
        {
            lock (SyncRoot)
            {
                var fileInfo = new FileInfo(fileName);

                if (!fileInfo.Exists)
                {
                    fileInfo.Directory.Create();
                }

                var serializer = new XmlSerializer(typeof(T));

                using (var writer = new XmlTextWriter(fileName, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 4;
                    serializer.Serialize(writer, t);
                }
            }
        }
    }
}