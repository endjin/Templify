namespace Endjin.Templify.Domain.Domain.Factories
{
    #region Using Directives

    using Endjin.Templify.Domain.Domain.Packages;
    using Endjin.Templify.Domain.Framework.Serialization;

    #endregion

    public static class ManifestFactory
    {
        public static void Save(string path, Manifest manifest)
        {
            Serializer.SaveInstance(manifest, path);
        }
    }
}