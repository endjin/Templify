namespace Endjin.Templify.Domain.Framework.Container
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    #endregion

    public static class MefContainer
    {
        public static void Compose(object parentContainer)
        {
            var catalog = new AggregateCatalog();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            catalog.Catalogs.Add(new DirectoryCatalog(baseDirectory));
            var container = new CompositionContainer(catalog);

            container.ComposeParts(parentContainer);
        }
    }
}